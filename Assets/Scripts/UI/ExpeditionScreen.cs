using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ExpeditionScreen : CityActionDisplay{
	public Button beginExpeditionButton;
	public GameObject townOptionPrefab;
	public Transform townOptionParents;
	[HideInInspector]public Town town;
	[HideInInspector]public TownsAndCities towns;

	List<TownOption> townOptions;
	Town selectedTown;

	void Start() {
		beginExpeditionButton.onClick.AddListener(BeginExpedition);
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

	void BeginExpedition() {
		CityActionFactory.DestroyCity();
		GlobalEvents.LocationPicked(selectedTown);
	}
}