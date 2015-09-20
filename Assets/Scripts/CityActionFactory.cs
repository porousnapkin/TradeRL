using UnityEngine;

public class CityActionFactory {
	public static Inventory inventory;
	public static GameObject marketPrefab;

	public static GameObject CreateMarketAction(Town t) {
		var marketGO = GameObject.Instantiate(marketPrefab);
		var display = marketGO.GetComponent<MarketDisplay>();
		display.inventory = inventory;
		display.town = t;

		return marketGO;
	}
}