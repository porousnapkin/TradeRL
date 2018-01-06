using strange.extensions.mediation.impl;

public class MarketDisplay : CityActionDisplay {
	public MarketSellDisplay sellDisplay;
	public TMPro.TextMeshProUGUI title;
	Town town;

	public void Setup(Inventory inventory, Town town) {
		this.town = town;

		title.text = "Markets of " + town.name;

		sellDisplay.inventory = inventory;
		sellDisplay.myTown = town;
	}
}

public class MarketDisplayMediator : Mediator {
	[Inject] public MarketDisplay view { private get; set; }
	[Inject] public Inventory inventory { private get; set; }
	[Inject] public Town town { private get; set; }

	public override void OnRegister ()
	{
		view.Setup(inventory, town);
	}
}