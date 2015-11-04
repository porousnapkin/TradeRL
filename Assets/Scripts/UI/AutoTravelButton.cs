using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoTravelButton : MonoBehaviour {
	public static AutoTravelButton instance = null;
	public static AutoTravelButton Instance { get { return instance; } }
	public PlayerController playerController;
	Town destination = null;

	void Awake() {
		instance = this;

		GetComponent<Button>().onClick.AddListener(TravelToLocation);

		TurnOff();
	}

	void TravelToLocation() {
		playerController.MouseOverPoint(destination.worldPosition);
		playerController.ClickedOnPosition(destination.worldPosition);
	}

	public void TurnOn(Town destination) {
		this.destination = destination;
		gameObject.SetActive(true);
	}

	public void TurnOff() {
		gameObject.SetActive(false);
	}
}
