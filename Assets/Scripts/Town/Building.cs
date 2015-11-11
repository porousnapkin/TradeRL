using UnityEngine;
using System.Collections;

public class Building {
	public int goldCost = 100;
	public Inventory inventory;
	public BuildingAbility buildingAbility;
	public Town town;
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
