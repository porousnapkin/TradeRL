using UnityEngine;
using UnityEngine.UI;

public class MarketSellTradeGoodDisplay : MonoBehaviour {
	public TMPro.TextMeshProUGUI title;	
	public TMPro.TextMeshProUGUI sellPriceText;
	public TMPro.TextMeshProUGUI sellOneButtonText;
	public TMPro.TextMeshProUGUI sellAllButtonText;
	public Button sellOneButton;
	public Button sellAllButton;
	[HideInInspector]public TradeGood tradeGood;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town activeTown;

	void Start() {
		SetupTextStrings();

		inventory.GoodsChangedEvent += TradeGoodsChanged;
		GlobalEvents.TownEconomyLeveldUpEvent += TownLeveledUp;

		sellOneButton.onClick.AddListener(SellOne); 
		sellAllButton.onClick.AddListener(SellAll); 
	}

	void OnDestroy() {
		inventory.GoodsChangedEvent -= TradeGoodsChanged;
		GlobalEvents.TownEconomyLeveldUpEvent -= TownLeveledUp;
	}

	void TownLeveledUp(Town t) {
		if(t == activeTown)
			SetupTextStrings();
	}

	void SellOne() {
		Sell (1);
	}

	void SellAll() {
		Sell(CalculateMaxSellable());
	}

	void Sell(int amount) {
		inventory.Gold += CalculateSellPrice() * amount;
		GlobalEvents.GoodsSoldEvent(amount, tradeGood, activeTown);
		inventory.LoseTradeGood(tradeGood.locationPurchased, amount);
	}

	void TradeGoodsChanged() {
		tradeGood = inventory.PeekAtGoods().Find(g => g == tradeGood);
		if(tradeGood == null)
			GameObject.Destroy(gameObject);
		else
			SetupTextStrings();
	}

	void SetupTextStrings() {
		title.text = tradeGood.quantity.ToString() + " goods from " + tradeGood.locationPurchased.name;
		var sellPrice = CalculateSellPrice();
		sellPriceText.text = sellPrice.ToString() + " gold";
		sellOneButtonText.text = "Sell 1 (" + sellPrice + " gold)";
		var maxSellable = CalculateMaxSellable();
		sellAllButtonText.text = "Sell " + maxSellable + " (" + (maxSellable * sellPrice) + " gold)";
	}

	int CalculateSellPrice() {
        return activeTown.economy.CalculatePriceTownPaysForGood(tradeGood);
	}

	int CalculateMaxSellable() {
        var sellPrice = CalculateSellPrice();
		return Mathf.Min(tradeGood.quantity, Mathf.FloorToInt(activeTown.economy.goldForPurchasingGoods.Available / sellPrice));
	}
}





