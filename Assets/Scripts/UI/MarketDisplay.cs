using UnityEngine;
using UnityEngine.UI;

public class MarketDisplay : CityActionDisplay {
	public MarketSellDisplay sellDisplay;
	public Text title;
	public Text goodsDemanded;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town town;

	void Start() {
		title.text = "Markets of " + town.name;

		sellDisplay.inventory = inventory;
		sellDisplay.myTown = town;

		Setup ();
		GlobalEvents.GoodsSoldEvent += Sold;
	}

	void OnDestroy() {
		GlobalEvents.GoodsSoldEvent -= Sold;
	}

	void Sold(int val1, TradeGood val2, Town val3) {
		Setup ();
	}

	void Setup() {
		goodsDemanded.text = "Goods Demanded: " + town.goodsDemanded + " / " + town.MaxGoodsDemanded;
	}
}