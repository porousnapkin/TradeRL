using UnityEngine;
using UnityEngine.UI;

public class TravelSupplyOption : MonoBehaviour
{
    public TMPro.TextMeshProUGUI itemName; 
    public TMPro.TextMeshProUGUI costText; 
    public Button purchaseButton;
    public UIImageRaycasterPopup popup;
    int affordabilityPopupSpace;
    PlayerCharacter playerCharacter;
    Inventory inventory;
    ItemData item;
    int cost;

    public void Setup(ItemData item, Inventory inventory, PlayerCharacter playerCharacter)
    {
        this.item = item;
        this.inventory = inventory;
        this.playerCharacter = playerCharacter;
        this.cost = item.standardPurchasePrice;

        itemName.text = item.name;
        costText.text = cost + " gold";
        popup.Record(item.itemDescription, popup.ReserveSpace());
        affordabilityPopupSpace = popup.ReserveSpace();

        purchaseButton.onClick.AddListener(PurchaseClicked);
        inventory.GoldChangedEvent += CheckAffordability;
        CheckAffordability();
    }

    void OnDestroy()
    {
        inventory.GoldChangedEvent -= CheckAffordability;
        purchaseButton.onClick.RemoveListener(PurchaseClicked);
    }

    void CheckAffordability()
    {
        bool canAfford = inventory.Gold >= item.standardPurchasePrice;
        purchaseButton.interactable = canAfford;

        if (canAfford)
            popup.Record("", affordabilityPopupSpace);
        else
            popup.Record("Not enough gold", affordabilityPopupSpace);

    }

    void PurchaseClicked()
    {
        inventory.Gold -= cost;
        inventory.AddItem(item.Create(playerCharacter.GetCharacter()));
    }
}

