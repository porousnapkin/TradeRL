using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class AutoTravelButton : DesertView {
	public static AutoTravelButton instance = null;
	public static AutoTravelButton Instance { get { return instance; } }
	public MapPlayerView playerController;
	Town destination = null;

	public event System.Action<Vector2> autoTravelHit = delegate{};

	protected override void Awake() {
		base.Awake();

		instance = this;

		GetComponent<Button>().onClick.AddListener(TravelToLocation);

		TurnOff();
	}

	void TravelToLocation() {
		autoTravelHit(destination.worldPosition);
	}

	public void TurnOn(Town destination) {
		this.destination = destination;
		gameObject.SetActive(true);
	}

	public void TurnOff() {
		gameObject.SetActive(false);
	}
}

public class AutoTravelButtonMediator : Mediator {
	[Inject] public AutoTravelButton view {private get; set; }
	[Inject] public MapPlayerController playerController  {private get; set; }

	public override void OnRegister ()
	{
		view.autoTravelHit += AutoTravel;
	}

	public override void OnRemove ()
	{
		view.autoTravelHit -= AutoTravel;
	}

	void AutoTravel(Vector2 position) {
		//TODO: IT would be nice to draw the path, but this is really debug so I may just kill this anyways...
		playerController.PathToPosition(position);
	}
}
