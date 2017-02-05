using UnityEngine.UI;
using strange.extensions.mediation.impl;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RestDisplay : CityActionDisplay
{
    public float daysPerHP = 1;
    public int goldPerDay = 1;
    public int effortPerDay = 5;
    public Button restUntilHealed;
    public Text text;
    //TODO: Should there be more rest options?
    //TODO: Rest until only player is healed?
    //TODO: Rest for 1 day?
    //TODO: Rest for 10 days?
    PlayerTeam playerTeam;
    PlayerCharacter playerCharacter;
    GameDate gameDate;
    Inventory inventory;
    Effort effort;
    int daysToFullyHeal;

    public void Setup(PlayerTeam playerTeam, PlayerCharacter playerCharacter, GameDate gameDate, Inventory inventory, Effort effort)
    {
        this.playerCharacter = playerCharacter;
        this.playerTeam = playerTeam;
        this.gameDate = gameDate;
        this.inventory = inventory;
        this.effort = effort;

        Recalculate();

        playerCharacter.GetCharacter().health.HealthChangedEvent += Recalculate;
        playerTeam.GetTeamCharacters().ForEach(c => c.health.HealthChangedEvent += Recalculate);
        restUntilHealed.onClick.AddListener(RestUntilHealed);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        playerCharacter.GetCharacter().health.HealthChangedEvent -= Recalculate;
        playerTeam.GetTeamCharacters().ForEach(c => c.health.HealthChangedEvent -= Recalculate);
        restUntilHealed.onClick.RemoveListener(RestUntilHealed);
    }

    void Recalculate()
    {
        var team = playerTeam.GetTeamCharacters();
        team.Add(playerCharacter.GetCharacter());
        var maxDaysForHealth = CalculateMostDaysToRecoverHealth(team);
        var maxDaysForEffort = CalculateDaysForEffort();
        daysToFullyHeal = Mathf.Max(maxDaysForEffort, maxDaysForHealth);
        var costInGold = (daysToFullyHeal * goldPerDay);

        text.text = "Rest Until Healed\n(" + daysToFullyHeal + " days, " + costInGold + " gold)";
        if (daysToFullyHeal <= 0 || costInGold > inventory.Gold)
            restUntilHealed.interactable = false;
    }

    int CalculateDaysForEffort()
    {
        var biggestDifference = Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental),
            Mathf.Min(GetEffortDistanceToFull(Effort.EffortType.Mental), GetEffortDistanceToFull(Effort.EffortType.Mental)));
        return Mathf.CeilToInt(biggestDifference * effortPerDay);
    }

    private float GetEffortDistanceToFull(Effort.EffortType type)
    {
        return effort.GetMaxEffort(type) - effort.GetEffort(type);
    }

    int CalculateMostDaysToRecoverHealth(List<Character> team)
    {
        return Mathf.CeilToInt(CalculateMostMissingHealth(team) * (float)daysPerHP);
    }

    int CalculateMostMissingHealth(List<Character> team)
    {
        return team.Max(c => c.health.MaxValue - c.health.Value);
    }

    void RestUntilHealed()
    {
        gameDate.AdvanceDays(daysToFullyHeal);

        FullyHeal(playerCharacter.GetCharacter());
        playerTeam.GetTeamCharacters().ForEach(c => FullyHeal(c));
        inventory.Gold -= daysToFullyHeal * goldPerDay;

        var effortRecovered = daysToFullyHeal / effortPerDay;
        effort.SafeAddEffort(Effort.EffortType.Mental, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Social, effortRecovered);
        effort.SafeAddEffort(Effort.EffortType.Physical, effortRecovered);
    }

    private void FullyHeal(Character character)
    {
        character.health.Heal(character.health.MaxValue);
    }
}

public class RestDisplayMediator : Mediator {
	[Inject] public RestDisplay view { private get; set; }
    [Inject] public PlayerTeam playerTeam { private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
    [Inject] public GameDate gameDate { private get; set; }
    [Inject] public Inventory inventory { private get; set; }
    [Inject] public Effort effort { private get; set; }

	public override void OnRegister ()
	{
		view.Setup(playerTeam, playerCharacter, gameDate, inventory, effort);
	}
}

