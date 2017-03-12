using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class RepairAllItemsButton : DesertView {
    public TMPro.TextMeshProUGUI costText;
    public Button button;
    public int costToRepair = 20;
    public UIImageRaycasterPopup popup;
    int popupSpace;
    Inventory inventory;

	public void Setup (Inventory inventory)
    {
        popupSpace = popup.ReserveSpace();
        costText.text = costToRepair.ToString() + " gold";
        button.onClick.AddListener(ButtonClicked);

        this.inventory = inventory;
        inventory.GoldChangedEvent += UpdateButton;
        UpdateButton();
	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        button.onClick.RemoveListener(ButtonClicked);
        inventory.GoldChangedEvent -= UpdateButton;
    }

    private void UpdateButton()
    {
        bool hasEnoughGold = inventory.Gold > costToRepair;
        var jammedItems = inventory.GetJammedItems();
        bool hasItemToRepair = jammedItems.Count > 0;

        button.interactable = hasEnoughGold && hasItemToRepair;

        if (!hasItemToRepair)
            popup.Record("No jammed items to repair", popupSpace);
        else if(!hasEnoughGold)
            popup.Record("Not enough gold", popupSpace);
    }

    private void ButtonClicked()
    {
        var jammedItems = inventory.GetJammedItems();
        jammedItems.ForEach(i => i.FixJam());

        inventory.Gold -= costToRepair;
    }
}

public class RepairAllItemsButtonMediator : Mediator
{
    [Inject]
    public Inventory inventory { private get; set; }
    [Inject]
    public RepairAllItemsButton view { private get; set; }

    public override void OnRegister()
    {
        view.Setup(inventory);
    }
}