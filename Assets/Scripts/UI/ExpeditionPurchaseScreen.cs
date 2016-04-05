using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;

public class ExpeditionPurchaseScreen : DesertView {
	public Button increaseSupplies;
	public Button decreaseSupplies;
	public Button increaseTradeGoods;
	public Button decreaseTradeGoods;
	public Text supliesTitle;
	public Text suppliesText;
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
	int suppliesToBuy = 10;
	int tradeGoodsToBuy = 10;
	public Signal destroyCitySignal = new Signal();
	public Signal<Town> beginExpedition = new Signal<Town>();

	protected override void Start() {
		SetupButtons ();
		inventory.GoldChangedEvent += GoldChanged;
		inventory.SuppliesChangedEvent += SuppliesChanged;

		UpdateToSuggested();
		UpdateState();
	}

	void OnEnable() {
		if(inventory != null)
			UpdateState ();
	}

	protected override void OnDestroy() {
		inventory.GoldChangedEvent -= GoldChanged;
		inventory.SuppliesChangedEvent -= SuppliesChanged;
	}

	void SuppliesChanged() {
		UpdateToSuggested();
		UpdateState();
	}

	void GoldChanged() {
		UpdateToSuggested();
		UpdateState();
	}

	public void UpdateToSuggested() {
		suppliesToBuy = GetSuggestedSupplies();	
		tradeGoodsToBuy = 0;
		var totalCost = CalculateCurrentTotalCost();
		tradeGoodsToBuy = Mathf.FloorToInt((inventory.Gold - totalCost) / CalculateTradeGoodPrice());
	}
	
	void SetupButtons() {
		ExtensionMethods.RefireButton(this, increaseSupplies, () => ChangeSupplies(1));
		ExtensionMethods.RefireButton(this, decreaseSupplies, () => ChangeSupplies(-1));
		ExtensionMethods.RefireButton(this, increaseTradeGoods, () => ChangeTradeGoods(1));
		ExtensionMethods.RefireButton(this, decreaseTradeGoods, () => ChangeTradeGoods(-1));

		beginButton.onClick.AddListener(BeginExpedition);
		backButton.onClick.AddListener(delegate() { gameObject.SetActive(false); previousScreen.SetActive(true); });
	}

	void ChangeSupplies(int val) {
		if(suppliesToBuy + val < 0)
			return;
		if(CalculateCurrentTotalCost() + (val * cost) > inventory.Gold)
			return;

		suppliesToBuy += val;	
		UpdateState();
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
		suppliesText.text = "Purchasing " + suppliesToBuy + " supplies\nfor " + (suppliesToBuy * cost) + " gold.";
		supliesTitle.text = "Current Supplies: " + inventory.Supplies;
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
		increaseSupplies.interactable = inventory.Gold >= totalCost + cost;
		decreaseSupplies.interactable = suppliesToBuy > 0;
		increaseTradeGoods.interactable = inventory.Gold >= totalCost + CalculateTradeGoodPrice();
		decreaseTradeGoods.interactable = tradeGoodsToBuy > 0;
	}

	int CalculateCurrentTotalCost() {
		int suppliesCost = cost * suppliesToBuy;
		int tradeGoodsCost = CalculateTradeGoodPrice() * tradeGoodsToBuy;

		return suppliesCost + tradeGoodsCost;
	}

	void BeginExpedition() {
		Debug.Log("Begin Expeditions");
		inventory.GainTradeGood(myTown, tradeGoodsToBuy, CalculateTradeGoodPrice());
		inventory.Supplies += suppliesToBuy;
		inventory.Gold -= CalculateCurrentTotalCost();

		GlobalEvents.GoodsPurchasedEvent(tradeGoodsToBuy, myTown);
		destroyCitySignal.Dispatch();
		beginExpedition.Dispatch(destinationTown);
	}

	int GetSuggestedSupplies() {
		return (int)Mathf.Min(Mathf.Max (GetEstimatedLength() - inventory.Supplies, 0), Mathf.FloorToInt(inventory.Gold / cost));
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
