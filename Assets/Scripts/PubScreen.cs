using UnityEngine;
using strange.extensions.mediation.impl;

public class PubScreen : CityActionDisplay {
	public Transform rumorsParent;
	public GameObject rumorsPrefab;
	Town town;
	TownsAndCities towns;
	Inventory inventory;
	GameDate gameDate;

	public void Setup(Town town, TownsAndCities towns, Inventory inventory, GameDate gameDate) {
		this.town = town;
		this.towns = towns;
		this.inventory = inventory;
		this.gameDate = gameDate;

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

public class PubScreenMediator : Mediator {
	[Inject]public Town town { private get; set; }
	[Inject]public TownsAndCities towns { private get; set; }
	[Inject]public Inventory inventory { private get; set; }
	[Inject]public GameDate gameDate { private get; set; }
	[Inject]public PubScreen view { private get; set; }

	public override void OnRegister ()
	{
		view.Setup(town, towns, inventory, gameDate);
	}
}
