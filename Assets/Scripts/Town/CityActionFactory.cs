using UnityEngine;

public class CityActionFactory {
	[Inject] public Inventory inventory {private get; set;}
	[Inject] public TownsAndCities townsAndCities {private get; set;}
	[Inject] public GameDate gameDate {private get; set;}
	GameObject activeCityGO;

	public GameObject CreateDisplayForCity(Town t) {
		townsAndCities.DiscoverLocation(t);

		DesertContext.QuickBind(t);
		var cityGO = GameObject.Instantiate(PrefabGetter.cityDisplayPrefab);
		DesertContext.FinishQuickBind<Town>();
		var display = cityGO.GetComponent<CityDisplay>();
		activeCityGO = cityGO;

		return cityGO;
	}

	//TODO: Review this at some point. should these be different classes rather than enums? Seems like it'd be simpler if they were...
	public GameObject CreateCityAction(CityAction action, Town t) {
		switch(action) {
			case CityAction.Center:
				return CreateCityCenter(t);
			case CityAction.Market: 
				return CreateMarketAction(t);
			case CityAction.Travel:
				return CreateExpiditionAction(t);
			case CityAction.Pub:
				return CreatePubAction(t);
			case CityAction.BuldingScene:
				return CreateBuildingScene(t);
			default:
				return null;
		}
	}

	public GameObject CreateCityCenter(Town t) {
		DesertContext.QuickBind(t);
		var cityCenterGO = GameObject.Instantiate(PrefabGetter.cityCenterPrefab);
		DesertContext.FinishQuickBind<Town>();

		return cityCenterGO;
	}

	public GameObject CreateMarketAction(Town t) {
		var marketGO = GameObject.Instantiate(PrefabGetter.marketPrefab);
		var display = marketGO.GetComponent<MarketDisplay>();
		display.inventory = inventory;
		display.town = t;
		var sell = marketGO.GetComponentInChildren<MarketSellDisplay>();
		sell.inventory = inventory;
		sell.myTown = t;

		return marketGO;
	}

	public GameObject CreateExpiditionAction(Town t) {
		var expiditionGO = GameObject.Instantiate(PrefabGetter.expeditionPrefab);
		var display = expiditionGO.GetComponent<ExpeditionScreen>();
		display.town = t;
		display.towns = townsAndCities;
		display.inventory = inventory;

		return expiditionGO;
	}

	public GameObject CreatePubAction(Town t) {
		var pubGO = GameObject.Instantiate(PrefabGetter.pubPrefab);
		var pubScreen = pubGO.GetComponent<PubScreen>();
		pubScreen.town = t;
		pubScreen.towns = townsAndCities;
		pubScreen.inventory = inventory;
		pubScreen.gameDate = gameDate;

		return pubGO;
	}

	public GameObject CreateBuildingScene(Town t) {
		var buildSceneGO = GameObject.Instantiate(PrefabGetter.buildingScenePrefab) as GameObject;
		var scene = buildSceneGO.GetComponent<BuildingScene>();
		scene.inventory = inventory;
		scene.town = t;

		return buildSceneGO;
	}

	public void DestroyCity() {
		Debug.Log ("Destroying city");
		GameObject.Destroy(activeCityGO);
	}
}