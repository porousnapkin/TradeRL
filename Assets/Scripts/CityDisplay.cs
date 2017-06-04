using UnityEngine;
using strange.extensions.mediation.impl;

public class CityDisplay : DesertView {
	public Transform cityScenesParent;
}

public class CityDisplayMediator : Mediator {
	[Inject] public CityDisplay view { private get; set; }
	[Inject] public CityActionFactory cityActionFactory { private get; set; }
	[Inject] public Town myTown { private get; set; }

	public override void OnRegister ()
	{
		var centerGO = (Resources.Load ("TownActions/Center") as CityActionData).Create(myTown);
		centerGO.transform.SetParent(view.cityScenesParent, false);
        centerGO.GetComponent<TownDialog>().SetupActions();
	}
}
