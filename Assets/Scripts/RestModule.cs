using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RestModule
{
    const float daysPerHP = 1;
    const int goldPerDay = 1;
    const int effortPerDay = 5;
    const int flatRateCost = 20;
    const int flatRateDays = 20;

    [Inject]
    public PlayerTeam playerTeam { private get; set; }
    [Inject]
    public PlayerCharacter playerCharacter { private get; set; }
    [Inject]
    public GameDate gameDate { private get; set; }
    [Inject]
    public Inventory inventory { private get; set; }
    [Inject]
    public Effort effort { private get; set; }

    public bool flatRate = true;
    public event System.Action<int> PlayerRestedForXDaysEvent = delegate { };

    public int GetDaysToFullyRecover()
    {
        var maxDaysForHealth = GetDaysToRecoverPlayersHealth();
        var maxDaysForEffort = GetDaysToRecoverEffort();
        var maxDaysForAllies = GetDaysToHealAllAllies(playerTeam.GetTeamCharacters());
        var calculatedDays = 0;

        if (flatRate)
            calculatedDays = flatRateDays;
        else
            calculatedDays = Mathf.Max(new int[3] { maxDaysForHealth, maxDaysForEffort, maxDaysForAllies });

        return calculatedDays;
    }

    public int GetDaysToRecoverPlayersHealth()
    {
        var health = playerCharacter.GetCharacter().health;
        return Mathf.CeilToInt((health.MaxValue - health.Value) * daysPerHP);
    }

    public int GetDaysToRecoverEffort()
    {
        var biggestDifference = Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental),
            Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental), GetEffortDistanceToFull(Effort.EffortType.Mental)));
        return Mathf.CeilToInt(biggestDifference * effortPerDay);
    }

    int GetEffortDistanceToFull(Effort.EffortType type)
    {
        return effort.GetMaxEffort(type) - effort.GetEffort(type);
    }

    public int GetDaysToHealAllAllies(List<Character> team)
    {
        return Mathf.CeilToInt(CalculateMostMissingHealth(team) * (float)daysPerHP);
    }

    int CalculateMostMissingHealth(List<Character> team)
    {
        if (team.Count == 0)
            return 0;
        return team.Max(c => c.health.MaxValue - c.health.Value);
    }

    public int GetCostToFullyRecover()
    {
        if (flatRate)
            return flatRateCost;
        else
            return GetDaysToFullyRecover() * goldPerDay;
    }

    //TODO: do we care about smaller rests anymore?

    public void RestUntilFullyRecovered()
    {
        var daysToAdvance = GetDaysToFullyRecover();
        gameDate.AdvanceDays(daysToAdvance);

        playerTeam.GetTeamCharacters().ForEach(c => FullyHeal(c));
        FullyHeal(playerCharacter.GetCharacter());

        foreach(Effort.EffortType effortType in Enum.GetValues(typeof(Effort.EffortType)))
            effort.SafeAddEffort(effortType, effort.GetMaxEffort(effortType));

        inventory.Gold -= GetCostToFullyRecover();

        PlayerRestedForXDaysEvent(daysToAdvance);
    }

    void FullyHeal(Character c)
    {
        c.health.Heal(c.health.MaxValue);
    }

    public void RestForDays(int days)
    {
        gameDate.AdvanceDays(days);

        playerTeam.GetTeamCharacters().ForEach(c => RestCharacterForDays(c, days));
        RestCharacterForDays(playerCharacter.GetCharacter(), days);

        var effortRecovered = days / effortPerDay;
        effort.SafeAddEffort(Effort.EffortType.Mental, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Social, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Physical, effortRecovered);

        inventory.Gold -= GetCostToFullyRecover();

        PlayerRestedForXDaysEvent(days);
    }

    void RestCharacterForDays(Character c, int days)
    {
        c.health.Heal(Mathf.FloorToInt(days / daysPerHP));
    }

}