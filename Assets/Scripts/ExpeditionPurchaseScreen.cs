using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;
using TMPro;

public class ExpeditionPurchaseScreen : CityActionDisplay {
	public Button increaseTradeGoods;
	public Button decreaseTradeGoods;
	public TextMeshProUGUI tradeGoodCost;
	public TextMeshProUGUI tradeGoodsAvailableText;
	public TextMeshProUGUI tradeGoodReplenishTime;
	public TextMeshProUGUI tradeGoodText;
	public Button purchaseButton;
    DailyReplenishingAsset tradeGoodsAvailable;
	Town myTown;
	Inventory inventory;
	int tradeGoodsToBuy = 10;

	public void Setup(Inventory inventory, Town myTown) {
        this.inventory = inventory;
        this.myTown = myTown;
        tradeGoodsAvailable = myTown.economy.goodsForSale;

        SetupButtons ();
		inventory.GoldChangedEvent += UpdateEverything;
        tradeGoodsAvailable.goodsPurchasedEvent += UpdateEverything;

        UpdateToSuggested();
		UpdateState();
	}

	void OnEnable() {
		if(inventory != null)
			UpdateState ();
	}

	protected override void OnDestroy() {
		inventory.GoldChangedEvent -= UpdateEverything;
        tradeGoodsAvailable.goodsPurchasedEvent -= UpdateEverything;
	}

	void UpdateEverything() {
		UpdateToSuggested();
		UpdateState();
	}

	public void UpdateToSuggested() {
		tradeGoodsToBuy = 0;
        tradeGoodsToBuy = MaxGoodsPurchasable();
	}

    public int MaxGoodsPurchasable()
    {
		var totalCost = CalculateCurrentTotalCost();
        var idealPurchasable = Mathf.FloorToInt((inventory.Gold - totalCost) / CalculateTradeGoodPrice());
        return Mathf.Min(idealPurchasable, tradeGoodsAvailable.Available);
    }
	
	void SetupButtons() {
		ExtensionMethods.RefireButton(this, increaseTradeGoods, () => ChangeTradeGoods(1));
		ExtensionMethods.RefireButton(this, decreaseTradeGoods, () => ChangeTradeGoods(-1));

        purchaseButton.onClick.AddListener(Purchase);
    }

    void Purchase()
    {
        var billToBuy = tradeGoodsToBuy;
        inventory.GainTradeGood(myTown, billToBuy, CalculateTradeGoodPrice());
        inventory.Gold -= CalculateCurrentTotalCost();
        tradeGoodsAvailable.Spend(billToBuy);

        GlobalEvents.GoodsPurchasedEvent(billToBuy, myTown);
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
        tradeGoodText.text = "Purchasing " + tradeGoodsToBuy + " trade goods";
		tradeGoodCost.text = "Costs " + CalculateTradeGoodPrice() + " gold";
        tradeGoodsAvailableText.text = tradeGoodsAvailable.Available.ToString() + " available";
        tradeGoodReplenishTime.text = "This city will create new trade goods in " + tradeGoodsAvailable.DaysTillReplenished + " days.";
        tradeGoodReplenishTime.gameObject.SetActive(tradeGoodsAvailable.IsReplenishing);
    }

    private int CalculateTradeGoodPrice()
    {
        return myTown.economy.CostOfTradeGood;
    }

    void UpdateButtons() {
		int totalCost = CalculateCurrentTotalCost();
		increaseTradeGoods.interactable = inventory.Gold >= totalCost + CalculateTradeGoodPrice() && tradeGoodsToBuy + 1 <= tradeGoodsAvailable.Available;
		decreaseTradeGoods.interactable = tradeGoodsToBuy > 0;
        purchaseButton.interactable = inventory.Gold >= totalCost;
	}

	int CalculateCurrentTotalCost() {
		int tradeGoodsCost = CalculateTradeGoodPrice() * tradeGoodsToBuy;

		return tradeGoodsCost;
	}
}

public class ExpeditionPurchaseScreenMediator : Mediator {
	[Inject] public ExpeditionPurchaseScreen view {private set; get; }
    [Inject] public Town town {private get; set; }
	[Inject] public Inventory inventory {private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();
        view.Setup(inventory, town);
    }
}
