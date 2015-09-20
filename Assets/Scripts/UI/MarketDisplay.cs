using UnityEngine;
using UnityEngine.UI;

public class MarketDisplay : MonoBehaviour {
	public MarketBuyDisplay buyDisplay;
	public MarketSellDisplay sellDisplay;
	public Text title;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Town town;

	void Start() {
		title.text = "Markets of " + town.name;

		buyDisplay.inventory = inventory;
		buyDisplay.myTown = town;

		sellDisplay.inventory = inventory;
		sellDisplay.myTown = town;
	}
}