using strange.extensions.mediation.impl;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CityRestButton : DesertView {
    const float daysPerHP = 1;
    const int goldPerDay = 1;
    const int effortPerDay = 5;
    const int flatRateCost = 20;
    const int flatRateDays = 20;

    public Button button;
    public TMPro.TextMeshProUGUI text;
    public UIImageRaycasterPopup popupInfo;

    public bool healMyHealth = true;
    public bool healMyEffort = false;
    public bool healMyAllies = false;
    public bool flatRate = false;

    int popupSpace;
    PlayerTeam playerTeam;
    PlayerCharacter playerCharacter;
    GameDate gameDate;
    Inventory inventory;
    Effort effort;
    int calculatedDays;

    public void Setup(PlayerTeam playerTeam, PlayerCharacter playerCharacter, GameDate gameDate, Inventory inventory, Effort effort)
    {
        this.playerCharacter = playerCharacter;
        this.playerTeam = playerTeam;
        this.gameDate = gameDate;
        this.inventory = inventory;
        this.effort = effort;
    }

    public void SetData()
    {
        popupSpace = popupInfo.ReserveSpace();

        Recalculate();
        playerCharacter.GetCharacter().health.HealthChangedEvent += Recalculate;
        playerTeam.GetTeamCharacters().ForEach(c => c.health.HealthChangedEvent += Recalculate);
        inventory.GoldChangedEvent += Recalculate;
        button.onClick.AddListener(Rest);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerCharacter.GetCharacter().health.HealthChangedEvent -= Recalculate;
        playerTeam.GetTeamCharacters().ForEach(c => c.health.HealthChangedEvent -= Recalculate);
        inventory.GoldChangedEvent -= Recalculate;
        button.onClick.RemoveListener(Rest);
    }

    void Recalculate()
    {
        var maxDaysForHealth = CalculateMostDaysToRecoverHealth();
        var maxDaysForEffort = CalculateDaysForEffort();
        var maxDaysForAllies = CalculateMaxDaysForAllies(playerTeam.GetTeamCharacters());
        calculatedDays = 0;

        if (healMyHealth)
            calculatedDays = Mathf.Max(calculatedDays, maxDaysForHealth);
        if(healMyEffort)
            calculatedDays = Mathf.Max(calculatedDays, maxDaysForEffort);
        if(healMyAllies)
            calculatedDays = Mathf.Max(calculatedDays, maxDaysForAllies);
        if (flatRate)
            calculatedDays = flatRateDays;
                
        var costInGold = (calculatedDays * goldPerDay);
        if (flatRate)
            costInGold = flatRateCost;

        text.text = "(" + calculatedDays + " days, " + costInGold + " gold)";
        button.interactable = calculatedDays > 0 && costInGold <= inventory.Gold;

        if (calculatedDays == 0)
            popupInfo.Record("Nothing to recover.", popupSpace);
        else if (costInGold > inventory.Gold)
            popupInfo.Record("Not enough gold to pay for rest.", popupSpace);
        else
            popupInfo.Record("", popupSpace);
    }

    int CalculateMostDaysToRecoverHealth()
    {
        var health = playerCharacter.GetCharacter().health;
        return Mathf.CeilToInt((health.MaxValue - health.Value) * daysPerHP);
    }

    int CalculateDaysForEffort()
    {
        var biggestDifference = Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental),
            Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental), GetEffortDistanceToFull(Effort.EffortType.Mental)));
        return Mathf.CeilToInt(biggestDifference * effortPerDay);
    }

    private int GetEffortDistanceToFull(Effort.EffortType type)
    {
        return effort.GetMaxEffort(type) - effort.GetEffort(type);
    }

    int CalculateMaxDaysForAllies(List<Character> team)
    {
        return Mathf.CeilToInt(CalculateMostMissingHealth(team) * (float)daysPerHP);
    }

    int CalculateMostMissingHealth(List<Character> team)
    {
        if (team.Count == 0)
            return 0;
        return team.Max(c => c.health.MaxValue - c.health.Value);
    }

    void Rest()
    {
        var daysToAdvance = calculatedDays;
        gameDate.AdvanceDays(daysToAdvance);

        playerTeam.GetTeamCharacters().ForEach(c => HealForDays(c, daysToAdvance));
        HealForDays(playerCharacter.GetCharacter(), daysToAdvance);

        var effortRecovered = daysToAdvance / effortPerDay;
        effort.SafeAddEffort(Effort.EffortType.Mental, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Social, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Physical, effortRecovered);

        inventory.Gold -= daysToAdvance * goldPerDay;
    }

    private void HealForDays(Character character, int days)
    {
        character.health.Heal(Mathf.FloorToInt(days / daysPerHP));
    }
}

public class CityRestButtonMediator : Mediator
{
    [Inject]
    public CityRestButton view { private get; set; }
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

    public override void OnRegister()
    {
        view.Setup(playerTeam, playerCharacter, gameDate, inventory, effort);
    }
}
