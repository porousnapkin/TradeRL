using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TownRumorButton : MonoBehaviour {
	public Text buttonText;
	public Button button;
	Town baseTown;
	Town town;
	TownsAndCities towns;
	Inventory inventory;
	GameDate gameDate;

	public void Setup(Town baseTown, Town town, TownsAndCities towns, Inventory inventory, GameDate gameDate) {
		this.town = town;
		this.towns = towns;
		this.baseTown = baseTown;
		this.inventory = inventory;
		this.gameDate = gameDate;
		bool isCity = !towns.CityList.Contains(town);

		buttonText.text = "A " + (isCity? "city" : "town") + " rumored to be  " + GetDistance() 
			+ " days away.\nIt will take " + GetDaysToGather() + " days and " + GetCost() + " gold to gather the information.";
		button.onClick.AddListener(ButtonHit);

		ButtonSetup();
		inventory.GoldChangedEvent += ButtonSetup;
	}

	void OnDestroy() {
		inventory.GoldChangedEvent -= ButtonSetup;
	}

	void ButtonSetup(int amount = 0) {
		button.interactable = inventory.Gold >= GetCost();
	}

	float GetDistance() {
		return Mathf.RoundToInt(Vector2.Distance(baseTown.worldPosition, town.worldPosition));
	}

	int GetCost() {
		return Mathf.FloorToInt(Mathf.Max (1, GetDistance() / 6.0f));
	}

	int GetDaysToGather() {	
		return Mathf.FloorToInt(Mathf.Max (1, GetDistance() / 8.0f));
	}

	void ButtonHit() {
		towns.DiscoverLocation(town);
		gameDate.AdvanceDays(GetDaysToGather());
		inventory.Gold -= GetCost();
		gameObject.SetActive(false);
	}
}
