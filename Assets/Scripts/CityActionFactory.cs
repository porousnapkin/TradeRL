using UnityEngine;

public class CityActionFactory {
	public static Inventory inventory;
	public static GameObject marketPrefab;
	public static GameObject cityCenterPrefab;
	public static GameObject cityDisplayPrefab;

	public static GameObject CreateDisplayForCity(Town t) {
		var cityGO = GameObject.Instantiate(cityDisplayPrefab);
		var display = cityGO.GetComponent<CityDisplay>();
		display.myTown = t;

		return cityGO;
	}

	public static GameObject CreateCityAction(CityAction action, Town t) {
		switch(action) {
			case CityAction.Center:
				return CreateCityCenter(t);
			case CityAction.Market: 
				return CreateMarketAction(t);
			default:
				return null;
		}
	}

	public static GameObject CreateCityCenter(Town t) {
		var cityCenterGO = GameObject.Instantiate(cityCenterPrefab);
		var td = cityCenterGO.GetComponent<TownDialog>();
		td.townToRepresent = t;

		return cityCenterGO;
	}

	public static GameObject CreateMarketAction(Town t) {
		var marketGO = GameObject.Instantiate(marketPrefab);
		var display = marketGO.GetComponent<MarketDisplay>();
		display.inventory = inventory;
		display.town = t;

		return marketGO;
	}

}