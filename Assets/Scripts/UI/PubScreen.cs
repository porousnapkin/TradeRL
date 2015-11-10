using UnityEngine;
using System.Collections;

public class PubScreen : CityActionDisplay {
	public Transform rumorsParent;
	public GameObject rumorsPrefab;
	[HideInInspector]public Town town;
	[HideInInspector]public TownsAndCities towns;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public GameDate gameDate;

	void Start() {
		FillRumors();
	}

	void FillRumors() {
		foreach(var t in town.rumoredLocations) {
			if(towns.KnownLocations.Contains(t))
				continue;

			var rumorsGO = GameObject.Instantiate(rumorsPrefab) as GameObject;
			rumorsGO.transform.SetParent(rumorsParent);

			rumorsGO.GetComponent<TownRumorButton>().Setup(town, t, towns, inventory, gameDate);
		}
	}
}
