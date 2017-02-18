using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class ExpeditionScreen : CityActionDisplay{
	public ExpeditionPurchaseScreen purchaseScreen;

	public void Setup(Town town, Inventory inventory) {
		purchaseScreen.myTown = town;
		purchaseScreen.inventory = inventory;
	}
}

public class ExpeditionScreenMediator : Mediator {
	[Inject] public ExpeditionScreen view {private get; set;}
	[Inject] public Town town {private get; set; }
	[Inject] public Inventory Inventory {private get; set; }

	public override void OnRegister ()
	{
		view.Setup(town, Inventory);
	}
}
