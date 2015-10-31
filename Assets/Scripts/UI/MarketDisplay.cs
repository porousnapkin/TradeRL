using UnityEngine;
using UnityEngine.UI;

public class MarketDisplay : CityActionDisplay {
	public MarketSellDisplay sellDisplay;
	public Text title;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town town;

	void Start() {
		title.text = "Markets of " + town.name;

		sellDisplay.inventory = inventory;
		sellDisplay.myTown = town;
	}
}