using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class BuildingScene : CityActionDisplay {
	public GameObject buildingPrefab;
	public Transform createBuildlingParents;
	public Transform activeBuildlingParents;
	Town town;
	Inventory inventory;

	public void Setup (Town t, Inventory inventory) {
		this.town = t;
		this.inventory = inventory;

		SetupVisuals ();
		GlobalEvents.BuildingBuilt += BuildingBuilt;
		town.playerBuildings.possibleBuildingGainedEvent += PossibleBuildingAdded;
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		GlobalEvents.BuildingBuilt -= BuildingBuilt;
		town.playerBuildings.possibleBuildingGainedEvent -= PossibleBuildingAdded;
	}

	void BuildingBuilt(Town t, Building b) {
		SetupVisuals ();
	}

	void PossibleBuildingAdded(Town t, Building b) {
		SetupVisuals ();
	}

	void SetupVisuals() {
		foreach(Transform child in createBuildlingParents)
			GameObject.Destroy (child.gameObject);
		foreach(Transform child in activeBuildlingParents)
			GameObject.Destroy (child.gameObject);

		foreach(var building in town.playerBuildings.unbuiltBuilding)
			SetupBuilding(createBuildlingParents, building);
		
		foreach(var building in town.playerBuildings.builtBuilding)
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

public class BuildingSceneMediator : Mediator {
	[Inject]public BuildingScene view { private get; set;}
	[Inject]public Town town { private get; set;}
	[Inject]public Inventory inventory { private get; set;}

	public override void OnRegister ()
	{
		view.Setup(town, inventory);
	}
}
