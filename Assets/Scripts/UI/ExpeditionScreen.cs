using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExpeditionScreen : CityActionDisplay{
	public Button beginExpeditionButton;
	public GameObject townOptionPrefab;
	public Transform townOptionParents;
	public GameObject pickDestinationGO;
	public GameObject suppliesSceenGO;
	public MarketBuyDisplay buyDisplay;
	public SuppliesBuyScreen suppliesDisplay;
	[HideInInspector]public Town town;
	[HideInInspector]public TownsAndCities towns;
	[HideInInspector]public Inventory inventory;

	List<TownOption> townOptions;
	Town selectedTown;

	void Start() {
		buyDisplay.myTown = town;
		buyDisplay.inventory = inventory;
		suppliesDisplay.myTown = town;
		suppliesDisplay.inventory = inventory;

		beginExpeditionButton.onClick.AddListener(LocationPicked);
		beginExpeditionButton.gameObject.SetActive(false);
		townOptions = new List<TownOption>();

		CreateTownOptions();
		GlobalEvents.TownDiscovered += TownDiscovered;
	}

	void OnDestroy() {
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
	}

	void TownSelected(TownOption option) {
		townOptions.ForEach(o => o.OnTownOptionSelected(option));
		selectedTown = option.representedTown;
		beginExpeditionButton.gameObject.SetActive(true);
		suppliesDisplay.destinationTown = selectedTown;
		buyDisplay.destinationTown = selectedTown;
	}

	void LocationPicked() {
		pickDestinationGO.SetActive(false);
		suppliesSceenGO.SetActive(true);
		GlobalEvents.LocationPickedEvent(selectedTown);
	}
}