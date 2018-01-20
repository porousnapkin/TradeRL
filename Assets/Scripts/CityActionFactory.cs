using UnityEngine;

public class CityActionFactory {
	[Inject] public Inventory inventory {private get; set;}
	[Inject] public Towns townsAndCities {private get; set;}
	[Inject] public GameDate gameDate {private get; set;}
	GameObject activeCityGO;

	public GameObject CreateDisplayForCity(Town t) {
		townsAndCities.DiscoverLocation(t);

		DesertContext.QuickBind(t);
		var cityGO = GameObject.Instantiate(PrefabGetter.cityDisplayPrefab);
		cityGO.transform.SetParent(PrefabGetter.baseCanvas, false);
		DesertContext.FinishQuickBind<Town>();

		activeCityGO = cityGO;

		return cityGO;
	}

	public void DestroyCity() {
		GameObject.Destroy(activeCityGO);
	}
}