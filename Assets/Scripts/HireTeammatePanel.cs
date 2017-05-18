using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class HireTeammatePanel : DesertView
{
    HireableAllyData hireableAllyData;
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI costText;
    public Image art;
    public UIImageRaycasterPopup popup;
    public Button button;
    int descriptionSpace;
    int unaffordableSpace;
    Inventory inventory;
    PlayerTeam playerTeam;

    public void SetupMediatedData(Inventory inventory, PlayerTeam playerTeam)
    {
        this.inventory = inventory;
        this.playerTeam = playerTeam;
    }

    public void SetupAlly(HireableAllyData ally)
    {
        this.hireableAllyData = ally;

        SetupPopup();
        SetupVisuals();
        inventory.GoldChangedEvent += UpdateDueToGold;
        UpdateDueToGold();

        button.onClick.AddListener(ButtonClicked);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        inventory.GoldChangedEvent -= UpdateDueToGold;
        button.onClick.RemoveListener(ButtonClicked);
    }

    private void SetupVisuals()
    {
        nameText.name = hireableAllyData.character.displayName;
        art.sprite = hireableAllyData.character.visuals;
        costText.text = "Hire: " + hireableAllyData.initialCost + "\nMaintain: " + hireableAllyData.costPerTrip;
    }

    private void SetupPopup()
    {
        descriptionSpace = popup.ReserveSpace();
        unaffordableSpace = popup.ReserveSpace();
        popup.Record(hireableAllyData.description + "\nHP: " + hireableAllyData.character.hp, descriptionSpace);
    }

    private void UpdateDueToGold()
    {
        if (inventory.Gold < hireableAllyData.initialCost)
            SetUnhireable();
        else
            SetHireable();
    }

    private void SetHireable()
    {
        button.interactable = true;
        popup.Record("", unaffordableSpace);
    }

    private void SetUnhireable()
    {
        button.interactable = false;
        popup.Record("Not enough gold to hire.", unaffordableSpace);
    }

    private void ButtonClicked()
    {
        inventory.Gold -= hireableAllyData.initialCost;
        playerTeam.AddAlly(hireableAllyData.character, hireableAllyData.getsWounded);
    }
}

public class HireTeammatePanelMediator : Mediator
{
    [Inject] public Inventory inventory { private get; set; }
    [Inject] public PlayerTeam playerTeam { private get; set; }
    [Inject] public HireTeammatePanel view { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        view.SetupMediatedData(inventory, playerTeam);
    }
}

