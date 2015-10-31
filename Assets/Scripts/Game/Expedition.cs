using UnityEngine;
using System.Collections;

public class Expedition {
	public Inventory inventory;
	public GameDate date;
	public PlayerController controller;
	public Character playerCharacter;
	bool starving = false;

	public void Begin() {
		controller.LocationEnteredEvent += HandleLocationEnteredEvent;
		date.DaysPassedEvent += HandleDaysPassedEvent; 
	}

	void HandleLocationEnteredEvent (Vector2 location) {
		date.AdvanceDays(1);
	}

	void HandleDaysPassedEvent (int days) {
		if(starving && inventory.Supplies <= 0) {
			playerCharacter.health.Damage(1);
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
	}
}
