
public class Expedition {
	[Inject] public MapData mapData { private get; set; }
	[Inject] public Inventory inventory { private get; set; }
	[Inject] public GameDate date { private get; set; }
	[Inject (Character.PLAYER)] public Character playerCharacter { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set; }

	bool starving = false;

	public void Begin(Town destination) {
		date.DaysPassedEvent += HandleDaysPassedEvent;
	}

	void HandleDaysPassedEvent (int days) {
		if(starving && inventory.Supplies <= 0) {
			playerCharacter.health.Damage(days);

			textArea.AddLine("Starving: -1 hp");
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
		date.DaysPassedEvent -= HandleDaysPassedEvent;
	}
}
