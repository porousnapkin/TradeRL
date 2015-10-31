using UnityEngine;
using UnityEngine.UI;

public class MarketBuyDisplay : MonoBehaviour {
	public Button buy1Button;
	public Button buy10Button;
	public Button buyMaxButton;
	public Button purchaseCamel;
	public Text buy1Text;
	public Text buy10Text;
	public Text buyMaxText;
	public Text purchaseCamelText;
	public Text maxCapacityText;
	public Text purchasePriceText;
	public Button beginButton;
	public Button backButton;
	public GameObject pickDestinationGO;
	public GameObject buyScreenGO;
	[HideInInspector]public Town myTown;
	[HideInInspector]public Inventory inventory;

	void Start() {
		inventory.GoodsChangedEvent += Setup;
		inventory.MaxGoodsCapacityChangedEvent += Setup;
		Setup();

		beginButton.onClick.AddListener(StartExpedition);
		backButton.onClick.AddListener(BackButtonHit);
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
		maxCapacityText.text = "Goods space: " + inventory.GetNumGoods() + " / " + inventory.MaxGoodsCapacity;
		var price = CalculatePurchasePrice();
		purchasePriceText.text = price.ToString() + " gold";

		buy1Button.onClick.RemoveAllListeners();
		buy1Button.onClick.AddListener(() => PurchaseTradeGoods(1));
		buy1Text.text = "Buy 1 (" + price + " gold)";
		buy1Button.interactable = price <= inventory.Gold;
		buy10Button.onClick.RemoveAllListeners();
		buy10Button.onClick.AddListener(() => PurchaseTradeGoods(10));
		buy10Text.text = "Buy 10 (" + (price*10) + " gold)";
		buy10Button.interactable = price*10 <= inventory.Gold;
		buyMaxButton.onClick.RemoveAllListeners();
		buyMaxButton.onClick.AddListener(() => PurchaseTradeGoods(spaceRemaining));
		buyMaxText.text = "Buy " + spaceRemaining + " (" + (spaceRemaining*price) + " gold)";
		buyMaxButton.interactable = price*spaceRemaining <= inventory.Gold;

		purchaseCamel.onClick.RemoveAllListeners();
		purchaseCamel.onClick.AddListener(PurchaseCamel);
		purchasePriceText.text = "Buy a Camel (" + CalculateCamelPrice() + " Gold, +" + CapacityPerCamel() + 
			" goods capacity)";
	}

	void PurchaseTradeGoods(int amount) {
		var price = CalculatePurchasePrice();
		inventory.Gold -= amount * price;
		inventory.GainTradeGood(myTown, amount, price);
	}

	void PurchaseCamel() {
		var price = CalculateCamelPrice();
		inventory.Gold -= price;
		inventory.MaxGoodsCapacity += CapacityPerCamel();
	}

	int CalculatePurchasePrice() {
		//TODO: figure this out...
		return 20;	
	}

	int CalculateCamelPrice () {
		//TODO: How will this work?
		return 50;
	}

	int CapacityPerCamel() {
		return 5;
	}

	void StartExpedition() {
		CityActionFactory.DestroyCity();
		ExpeditionFactory.BeginExpedition();
	}

	void BackButtonHit() {
		pickDestinationGO.SetActive(true);
		buyScreenGO.SetActive(false);
	}
}