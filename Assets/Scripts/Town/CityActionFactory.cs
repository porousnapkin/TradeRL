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

		activeCityGO = cityGO;

		return cityGO;
	}

	public void DestroyCity() {
		Debug.Log ("Destroying city");
		GameObject.Destroy(activeCityGO);
	}
}