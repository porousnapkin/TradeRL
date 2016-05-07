using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class ExpeditionPurchaseScreen : DesertView {
	public Button increaseTradeGoods;
	public Button decreaseTradeGoods;
	public Text tradeGoodTitle;
	public Text tradeGoodText;
	public Text totalCost;
	public Button beginButton;
	public Button backButton;
	public GameObject previousScreen;
	[HideInInspector]public Town myTown;
	[HideInInspector]public Town destinationTown;
	[HideInInspector]public Inventory inventory;
	int cost = 1;
	int tradeGoodsToBuy = 10;
	public Signal destroyCitySignal = new Signal();
	public Signal<Town> beginExpedition = new Signal<Town>();

	protected override void Start() {
		SetupButtons ();
		inventory.GoldChangedEvent += GoldChanged;

		UpdateToSuggested();
		UpdateState();
	}

	void OnEnable() {
		if(inventory != null)
			UpdateState ();
	}

	protected override void OnDestroy() {
		inventory.GoldChangedEvent -= GoldChanged;
	}

	void GoldChanged() {
		UpdateToSuggested();
		UpdateState();
	}

	public void UpdateToSuggested() {
		tradeGoodsToBuy = 0;
		var totalCost = CalculateCurrentTotalCost();
		tradeGoodsToBuy = Mathf.FloorToInt((inventory.Gold - totalCost) / CalculateTradeGoodPrice());
	}
	
	void SetupButtons() {
		ExtensionMethods.RefireButton(this, increaseTradeGoods, () => ChangeTradeGoods(1));
		ExtensionMethods.RefireButton(this, decreaseTradeGoods, () => ChangeTradeGoods(-1));

		beginButton.onClick.AddListener(BeginExpedition);
		backButton.onClick.AddListener(delegate() { gameObject.SetActive(false); previousScreen.SetActive(true); });
	}

	void ChangeTradeGoods(int val) {
		if(tradeGoodsToBuy + val < 0)
			return;
		if(CalculateCurrentTotalCost() + (val * CalculateTradeGoodPrice()) > inventory.Gold)
			return;

		tradeGoodsToBuy += val;
		UpdateState();
	}

	public void UpdateState() {
		UpdateText();
		UpdateButtons();
	}

	void UpdateText() {
		tradeGoodText.text = "Purchasing " + tradeGoodsToBuy + " trade goods\nfor " + (tradeGoodsToBuy * CalculateTradeGoodPrice()) + " gold.";
		tradeGoodTitle.text = "Trade Goods\nCosts " + CalculateTradeGoodPrice() + " gold";
		totalCost.text = "Total Cost: " + CalculateCurrentTotalCost() + " / " + inventory.Gold;
	}

	int CalculateTradeGoodPrice() {
		//TODO: figure this out...
		return 20;	
	}

	void UpdateButtons() {
		int totalCost = CalculateCurrentTotalCost();
		increaseTradeGoods.interactable = inventory.Gold >= totalCost + CalculateTradeGoodPrice();
		decreaseTradeGoods.interactable = tradeGoodsToBuy > 0;
	}

	int CalculateCurrentTotalCost() {
		int tradeGoodsCost = CalculateTradeGoodPrice() * tradeGoodsToBuy;

		return tradeGoodsCost;
	}

	void BeginExpedition() {
		Debug.Log("Begin Expeditions");
		inventory.GainTradeGood(myTown, tradeGoodsToBuy, CalculateTradeGoodPrice());
		inventory.Gold -= CalculateCurrentTotalCost();

		GlobalEvents.GoodsPurchasedEvent(tradeGoodsToBuy, myTown);
		destroyCitySignal.Dispatch();
		beginExpedition.Dispatch(destinationTown);
	}

	int GetEstimatedLength() {
		return Mathf.RoundToInt(Vector2.Distance(myTown.worldPosition, destinationTown.worldPosition) * 1.25f);
	}
}

public class ExpeditionPurchaseScreenMediator : Mediator {
	[Inject] public ExpeditionPurchaseScreen view {private set; get; }
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
