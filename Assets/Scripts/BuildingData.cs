using UnityEngine;
using System.Collections;

public class BuildingData : ScriptableObject {
	public BuildingAbilityData ability;
	public int cost = 100;

	public Building Create(Town t) {
		var b = DesertContext.StrangeNew<Building>();

		b.buildingAbility = ability.Create();
		b.goldCost = cost;
		b.town = t;

		return b;
	}
}
