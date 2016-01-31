using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class MarketBuyDisplay : DesertView {
	public Signal<Town> beginExpedition = new Signal<Town>();
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
	public Text supply;
	public Button beginButton;
	public Button backButton;
	public GameObject previousGO;
	public GameObject nextGO;
	[HideInInspector]public Town myTown;
	[HideInInspector]public Town destinationTown;
	[HideInInspector]public Inventory inventory;
	public Signal destroyCitySignal = new Signal();

	protected override void Start() {
		base.Start();

		inventory.GoodsChangedEvent += Setup;
		inventory.MaxGoodsCapacityChangedEvent += Setup;
		Setup();

		beginButton.onClick.AddListener(StartExpedition);
		backButton.onClick.AddListener(BackButtonHit);
	}

	protected override void OnDestroy() {
		base.OnDestroy();

		inventory.GoodsChangedEvent -= Setup;
		inventory.MaxGoodsCapacityChangedEvent -= Setup;
	}

	void OnEnable() {
		Setup();
	}

	void Setup() {
		maxCapacityText.text = "Goods space: " + inventory.GetNumGoods() + " / " + inventory.MaxGoodsCapacity;
		var price = CalculatePurchasePrice();
		purchasePriceText.text = price.ToString() + " gold";
		supply.text = "Goods Supply: " + myTown.SupplyGoods + " / " + myTown.MaxGoodsSurplus;

		buy1Button.onClick.RemoveAllListeners();
		buy1Button.onClick.AddListener(() => PurchaseTradeGoods(1));
		buy1Text.text = "Buy 1 (" + price + " gold)";
		buy1Button.interactable = price <= inventory.Gold && myTown.SupplyGoods >= 1 && inventory.RemainingGoodsSpace >= 1;
		buy10Button.onClick.RemoveAllListeners();
		buy10Button.onClick.AddListener(() => PurchaseTradeGoods(10));
		buy10Text.text = "Buy 10 (" + (price*10) + " gold)";
		buy10Button.interactable = price*10 <= inventory.Gold && myTown.SupplyGoods >= 10 && inventory.RemainingGoodsSpace >= 10;
		
		int amount = Mathf.Min(inventory.RemainingGoodsSpace, Mathf.FloorToInt(inventory.Gold / price) );
		amount = Mathf.Min (myTown.SupplyGoods, amount);
		buyMaxButton.onClick.RemoveAllListeners();
		buyMaxButton.onClick.AddListener(() => PurchaseTradeGoods(amount));
		buyMaxText.text = "Buy " + amount + " (" + (amount * price) + " gold)";
		buyMaxButton.interactable = price * amount <= inventory.Gold && amount > 0 && inventory.RemainingGoodsSpace >= amount;

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
		GlobalEvents.GoodsPurchasedEvent(amount, myTown);
		Setup ();
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
		return 40;
	}

	int CapacityPerCamel() {
		return Inventory.maxGoodsPerCamel;
	}

	void StartExpedition() {
		destroyCitySignal.Dispatch();
		beginExpedition.Dispatch(destinationTown);
	}

	void BackButtonHit() {
		previousGO.SetActive(true);
		nextGO.SetActive(false);
	}
}

public class MarketBuyDisplayMediator : Mediator {
	[Inject] public MarketBuyDisplay view {private set; get; }
	[Inject] public CityActionFactory cityActionFactory {private get; set; }
	[Inject] public ExpeditionFactory expeditionFactory { private get; set; }

	public override void OnRegister ()
	{
		view.beginExpedition.AddListener(expeditionFactory.BeginExpedition);
		view.destroyCitySignal.AddListener(DestroyCity);
	}
	
	public override void OnRemove() 
	{
		view.beginExpedition.RemoveListener(expeditionFactory.BeginExpedition);
		view.destroyCitySignal.RemoveListener(DestroyCity);
	}

	void DestroyCity() 
	{
		cityActionFactory.DestroyCity();
	}
}
