using UnityEngine;
using UnityEngine.UI;

public class MarketBuyDisplay : MonoBehaviour {
	public Button buy1Button;
	public Button buy10Button;
	public Button buyMaxButton;
	public Text buy1Text;
	public Text buy10Text;
	public Text buyMaxText;
	public Text maxCapacityText;
	public Text purchasePriceText;

	[HideInInspector]public Town myTown;
	[HideInInspector]public Inventory inventory;

	void Start() {
		inventory.GoodsChangedEvent += Setup;
		inventory.MaxGoodsCapacityChangedEvent += Setup;
		Setup();
	}

	void OnDestroy() {
		inventory.GoodsChangedEvent -= Setup;
		inventory.MaxGoodsCapacityChangedEvent -= Setup;
	}

	void OnEnable() {
		Setup();
	}

	void Setup() {
		int spaceRemaining = inventory.MaxGoodsCapacity - inventory.GetNumGoods();
		maxCapacityText.text = "Inventory space remaining: " + spaceRemaining + " / " + inventory.MaxGoodsCapacity;
		var price = CalculatePurchasePrice();
		purchasePriceText.text = price.ToString() + " gold";

		buy1Button.onClick.RemoveAllListeners();
		buy1Button.onClick.AddListener(() => Purchase(1));
		buy1Text.text = "Buy 1 (" + price + " gold)";
		buy1Button.interactable = price <= inventory.Gold;
		buy10Button.onClick.RemoveAllListeners();
		buy10Button.onClick.AddListener(() => Purchase(10));
		buy10Text.text = "Buy 10 (" + (price*10) + " gold)";
		buy10Button.interactable = price*10 <= inventory.Gold;
		buyMaxButton.onClick.RemoveAllListeners();
		buyMaxButton.onClick.AddListener(() => Purchase(spaceRemaining));
		buyMaxText.text = "Buy " + spaceRemaining + " (" + (spaceRemaining*price) + " gold)";
		buyMaxButton.interactable = price*spaceRemaining <= inventory.Gold;
	}

	void Purchase(int amount) {
		var price = CalculatePurchasePrice();
		inventory.Gold -= amount * CalculatePurchasePrice();
		inventory.GainTradeGood(myTown, amount, price);
	}

	int CalculatePurchasePrice() {
		//TODO: figure this out...
		return 20;	
	}
}