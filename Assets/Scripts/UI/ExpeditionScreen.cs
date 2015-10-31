using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExpeditionScreen : CityActionDisplay{
	public Button beginExpeditionButton;
	public GameObject townOptionPrefab;
	public Transform townOptionParents;
	public GameObject pickDestinationGO;
	public GameObject buyScreenGO;
	public MarketBuyDisplay buyDisplay;
	[HideInInspector]public Town town;
	[HideInInspector]public TownsAndCities towns;
	[HideInInspector]public Inventory inventory;

	List<TownOption> townOptions;
	Town selectedTown;

	void Start() {
		buyDisplay.myTown = town;
		buyDisplay.inventory = inventory;

		beginExpeditionButton.onClick.AddListener(LocationPicked);
		beginExpeditionButton.gameObject.SetActive(false);
		townOptions = new List<TownOption>();

		var knownLocations = towns.KnownLocations;
		knownLocations.Sort((first, second) => Mathf.RoundToInt(Vector3.Distance(first.worldPosition, town.worldPosition) - 
			Vector3.Distance(second.worldPosition, town.worldPosition)));
		knownLocations.Remove(town);

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
	}

	void LocationPicked() {
		pickDestinationGO.SetActive(false);
		buyScreenGO.SetActive(true);
		GlobalEvents.LocationPicked(selectedTown);
	}
}