using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class ExpeditionScreen : CityActionDisplay{
	public Button beginExpeditionButton;
	public GameObject townOptionPrefab;
	public Transform townOptionParents;
	public GameObject pickDestinationGO;
	public GameObject purchaseSceenGO;
	public ExpeditionPurchaseScreen purchaseScreen;
	Town town;
	TownsAndCities towns;

	List<TownOption> townOptions;
	Town selectedTown;

	public void Setup(Town town, TownsAndCities towns, Inventory inventory) {
		this.town = town;
		this.towns = towns;

		purchaseScreen.myTown = town;
		purchaseScreen.inventory = inventory;

		beginExpeditionButton.onClick.AddListener(LocationPicked);
		beginExpeditionButton.gameObject.SetActive(false);
		townOptions = new List<TownOption>();

		CreateTownOptions();
		GlobalEvents.TownDiscovered += TownDiscovered;
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		GlobalEvents.TownDiscovered -= TownDiscovered;
	}

	void TownDiscovered(Town t) {
		CreateTownOptions();
	}

	void CreateTownOptions() {
		var knownLocations = towns.KnownLocations;
		knownLocations.Sort((first, second) => Mathf.RoundToInt(Vector3.Distance(first.worldPosition, town.worldPosition) - 
		                                                        Vector3.Distance(second.worldPosition, town.worldPosition)));
		knownLocations.Remove(town);

		foreach(Transform t in townOptionParents)
			GameObject.Destroy(t.gameObject);
		
		for(int i = 0; i < knownLocations.Count; i++) {
			var townOptionGO = GameObject.Instantiate(townOptionPrefab);
			townOptionGO.transform.SetParent(townOptionParents, false);
			
			var townOption = townOptionGO.GetComponent<TownOption>();
			townOption.representedTown = knownLocations[i];
			townOption.startTown = town;
			townOption.TownSelectedEvent += TownSelected;
			townOptions.Add(townOption);
		}

		townOptions[0].Select();
	}

	void TownSelected(TownOption option) {
		townOptions.ForEach(o => o.OnTownOptionSelected(option));
		selectedTown = option.representedTown;
		beginExpeditionButton.gameObject.SetActive(true);
		purchaseScreen.destinationTown = selectedTown;
		purchaseScreen.UpdateToSuggested();
	}

	void LocationPicked() {
		pickDestinationGO.SetActive(false);
		purchaseSceenGO.SetActive(true);
		GlobalEvents.LocationPickedEvent(selectedTown);
	}
}

public class ExpeditionScreenMediator : Mediator {
	[Inject] public ExpeditionScreen view {private get; set;}
	[Inject] public Town town {private get; set; }
	[Inject] public TownsAndCities towns {private get; set; }
	[Inject] public Inventory Inventory {private get; set; }

	public override void OnRegister ()
	{
		view.Setup(town, towns, Inventory);
	}
}
