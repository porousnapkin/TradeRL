using UnityEngine;
using System.Collections;

public class BuildingScene : CityActionDisplay {
	public GameObject buildingPrefab;
	public Transform createBuildlingParents;
	public Transform activeBuildlingParents;
	[HideInInspector]public Town town;
	[HideInInspector]public Inventory inventory;

	void Start () {
		Setup ();
		GlobalEvents.BuildingBuilt += BuildingBuilt;
		town.possibleBuildingGainedEvent += PossibleBuildingAdded;
	}

	void OnDestroy() {
		GlobalEvents.BuildingBuilt -= BuildingBuilt;
		town.possibleBuildingGainedEvent -= PossibleBuildingAdded;
	}

	void BuildingBuilt(Town t, Building b) {
		Setup ();
	}

	void PossibleBuildingAdded(Town t, Building b) {
		Setup ();
	}

	void Setup() {
		foreach(Transform child in createBuildlingParents)
			GameObject.Destroy (child.gameObject);
		foreach(Transform child in activeBuildlingParents)
			GameObject.Destroy (child.gameObject);

		foreach(var building in town.unbuiltBuilding)
			SetupBuilding(createBuildlingParents, building);
		
		foreach(var building in town.builtBuilding)
			SetupBuilding(activeBuildlingParents, building);
	}

	void SetupBuilding(Transform parent, Building b) {
		var buildGO = GameObject.Instantiate(buildingPrefab) as GameObject;
		buildGO.transform.SetParent(parent);

		var buildButton = buildGO.GetComponent<BuildingButton>();
		buildButton.inventory = inventory;
		buildButton.building = b;
	}
}
