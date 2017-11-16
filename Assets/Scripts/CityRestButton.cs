using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class CityRestButton : DesertView {
    public Button button;
    public TMPro.TextMeshProUGUI text;
    public UIImageRaycasterPopup popupInfo;

    public bool flatRate = false;

    int popupSpace;
    PlayerTeam playerTeam;
    PlayerCharacter playerCharacter;
    Inventory inventory;
    RestModule rest;
    int calculatedDays;

    public void Setup(PlayerTeam playerTeam, PlayerCharacter playerCharacter, Inventory inventory, Town town)
    {
        this.playerCharacter = playerCharacter;
        this.playerTeam = playerTeam;
        this.inventory = inventory;
        this.rest = town.restModule;
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
        var calculatedDays = rest.GetDaysToFullyRecover();
        var cost = rest.GetCostToFullyRecover();

        text.text = "(" + calculatedDays + " days, " + cost + " gold)";
        button.interactable = calculatedDays > 0 && cost <= inventory.Gold;

        if (calculatedDays == 0)
            popupInfo.Record("Nothing to recover.", popupSpace);
        else if (cost > inventory.Gold)
            popupInfo.Record("Not enough gold to pay for rest.", popupSpace);
        else
            popupInfo.Record("", popupSpace);
    }

    void Rest()
    {
        rest.RestUntilFullyRecovered();
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
    public Inventory inventory { private get; set; }
	[Inject] public Town town { private get; set; }

    public override void OnRegister()
    {
        view.Setup(playerTeam, playerCharacter, inventory, town);
    }
}
