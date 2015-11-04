using UnityEngine;
using System.Collections;

public class Expedition {
	public Inventory inventory;
	public GameDate date;
	public PlayerController controller;
	public Character playerCharacter;
	public MapCreator mapCreator;
	bool starving = false;

	public void Begin(Town destination) {
		controller.LocationEnteredEvent += HandleLocationEnteredEvent;
		date.DaysPassedEvent += HandleDaysPassedEvent;
		AutoTravelButton.Instance.TurnOn(destination);
	}

	void HandleLocationEnteredEvent (Vector2 location) {
		if(mapCreator.IsHill(location))
			date.AdvanceDays(2);
		else
			date.AdvanceDays(1);
	}

	void HandleDaysPassedEvent (int days) {
		if(starving && inventory.Supplies <= 0) {
			playerCharacter.health.Damage(days);
		}
		else if(inventory.Supplies <= days) {
			int daysRemaining = days - inventory.Supplies;
			inventory.Supplies = 0;
			starving = true;
			if(daysRemaining > 0)
				HandleDaysPassedEvent(daysRemaining);
		}
		else {
			inventory.Supplies -= days;
		}
	}

	public void Finish() {
		controller.LocationEnteredEvent -= HandleLocationEnteredEvent;
		date.DaysPassedEvent -= HandleDaysPassedEvent;
		AutoTravelButton.Instance.TurnOff();
	}
}
