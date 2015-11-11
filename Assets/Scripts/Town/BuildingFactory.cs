using UnityEngine;
using System.Collections;

public class BuildingFactory {
	public static GameDate gameDate;
	public static Inventory inventory;

	public static Building CreateTradePost(Town whereBuilt) {
		TradingPost tp = new TradingPost();
		tp.gameDate = gameDate;
		tp.inventory = inventory;

		return CreateBuilding(tp, 300, whereBuilt);
	}

	static Building CreateBuilding(BuildingAbility ability, int cost, Town whereBuilt) {
		Building b = new Building();
		b.inventory = inventory;
		b.buildingAbility = ability;
		b.goldCost = cost;
		b.town = whereBuilt;
		
		return b;
	}
}
