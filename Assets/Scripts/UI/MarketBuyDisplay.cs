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
	public Text camelsText;
	public Button beginButton;
	public Button backButton;
	public GameObject previousGO;
	public GameObject nextGO;
	[HideInInspector]public Town myTown;
	[HideInInspector]public Town destinationTown;
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
		
		int amount = Mathf.Min(spaceRemaining, Mathf.FloorToInt(inventory.Gold / price) );
		buyMaxButton.onClick.RemoveAllListeners();
		buyMaxButton.onClick.AddListener(() => PurchaseTradeGoods(amount));
		buyMaxText.text = "Buy " + amount + " (" + (amount * price) + " gold)";
		buyMaxButton.interactable = price * amount <= inventory.Gold && amount > 0;

		camelsText.text = "Camels: " + inventory.Camels;

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
		inventory.AddACamel(1);
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
		return Inventory.maxGoodsPerCamel;
	}

	void StartExpedition() {
		CityActionFactory.DestroyCity();
		ExpeditionFactory.BeginExpedition(destinationTown);
	}

	void BackButtonHit() {
		previousGO.SetActive(true);
		nextGO.SetActive(false);
	}
}