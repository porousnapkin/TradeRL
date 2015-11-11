using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingButton : MonoBehaviour {
	public Text text;
	public Button button;
	[HideInInspector]public Inventory inventory;
	[HideInInspector]public Building building;

	void Start() {
		inventory.GoldChangedEvent += Setup;
		Setup ();

		button.onClick.AddListener(Clicked);
	}

	void OnDestroy() {
		inventory.GoldChangedEvent -= Setup;
	}

	void Setup(int amount = 0) {
		button.interactable = building.IsBuilt() || building.CanBuild();
		
		text.text = building.Describe();
	}

	void Clicked() {
		if(building.IsBuilt())
			building.Activate();
		else
			building.Build();
	}
}
