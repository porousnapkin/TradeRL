using UnityEngine;
using System.Collections;

#warning "We can create a data class for buildings, then we won't need this factory."
public static class BuildingFactory {
	public static Building CreateBuilding(BuildingAbility ability, int cost, Town whereBuilt) {
		Building b = DesertContext.StrangeNew<Building>();
		b.buildingAbility = ability;
		b.goldCost = cost;
		b.town = whereBuilt;
		
		return b;
	}
}
