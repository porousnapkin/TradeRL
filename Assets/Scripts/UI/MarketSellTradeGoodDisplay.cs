using UnityEngine;
using UnityEngine.UI;

public class MarketSellTradeGoodDisplay : MonoBehaviour {
	public Text title;	
	public Text purchasePriceText;
	public Text sellPriceText;
	public Text sellOneButtonText;
	public Text sellAllButtonText;
	public Button sellOneButton;
	public Button sellAllButton;

	[HideInInspector]public TradeGood tradeGood;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town activeTown;

	void Start() {
		SetupTextStrings();

		inventory.GoodsChangedEvent += TradeGoodsChanged;

		sellOneButton.onClick.AddListener(SellOne); 
		sellAllButton.onClick.AddListener(SellAll); 
	}

	void OnDestroy() {
		inventory.GoodsChangedEvent -= TradeGoodsChanged;
	}

	void SellOne() {
		inventory.Gold += CalculateSellPrice();
		inventory.LoseTradeGood(tradeGood.locationPurchased, 1);
	}

	void SellAll() {
		int maxSellable = CalculateMaxSellable();
		inventory.Gold += CalculateSellPrice() * maxSellable;
		inventory.LoseTradeGood(tradeGood.locationPurchased, maxSellable);
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
		purchasePriceText.text = tradeGood.purchasePrice.ToString() + " gold";
		var sellPrice = CalculateSellPrice();
		sellPriceText.text = sellPrice.ToString() + " gold";
		sellOneButtonText.text = "Sell 1 (" + sellPrice + " gold)";
		var maxSellable = CalculateMaxSellable();
		sellAllButtonText.text = "Sell " + maxSellable + " (" + (maxSellable * sellPrice) + " gold)";
	}

	int CalculateSellPrice() {
		//TODO: figure this out...
		if(tradeGood.locationPurchased == activeTown)
			return 20;
		return 30;
	}

	int CalculateMaxSellable() {
		//TODO: probably capped by city?
		return tradeGood.quantity;
	}
}





