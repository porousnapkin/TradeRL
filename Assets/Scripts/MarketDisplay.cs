using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class MarketDisplay : CityActionDisplay {
	public MarketSellDisplay sellDisplay;
	public Text title;
	public Text goodsDemanded;
	Town town;

	public void Setup(Inventory inventory, Town town) {
		this.town = town;

		title.text = "Markets of " + town.name;

		sellDisplay.inventory = inventory;
		sellDisplay.myTown = town;

		FixText ();
		GlobalEvents.GoodsSoldEvent += Sold;
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		GlobalEvents.GoodsSoldEvent -= Sold;
	}

	void Sold(int val1, TradeGood val2, Town val3) {
		FixText ();
	}

	void FixText() {
		goodsDemanded.text = "Goods Demanded: " + town.goodsDemanded + " / " + town.MaxGoodsDemanded;
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