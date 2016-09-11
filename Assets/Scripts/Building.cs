using UnityEngine;
using System.Collections;

public class Building {
	[Inject] public Inventory inventory  { private get; set; }
	public int goldCost { private get; set; }
	public BuildingAbility buildingAbility  { private get; set; }
	public Town town  { private get; set; }
	bool isBuilt = false;

	public bool CanBuild() {
		return inventory.Gold > goldCost;
	}

	public void Build() {
		inventory.Gold -= goldCost;
		isBuilt = true;
		buildingAbility.Build();
		town.BuildBuilding(this);

		GlobalEvents.BuildingBuilt(town, this);
	}

	public string Describe() {
		string baseDescription = "";

		if(isBuilt)
			baseDescription = buildingAbility.DescribeBuilt();
		else
			baseDescription = buildingAbility.DescribeUnbuilt() + " Costs "+ goldCost + " gold.";

		return baseDescription;
	}

	public void Activate() {
		buildingAbility.ActivateBuilt();
	}

	public bool IsBuilt() {
		return isBuilt;
	}
}
