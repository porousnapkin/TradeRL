using UnityEngine;
using System.Collections;

public class TradingPost : BuildingAbility {
	public GameDate gameDate;
	public Inventory inventory;
	float goldPerDay = 0.2f;
	float floatGoldAccrued = 0;
	int realGoldAccrued = 0;
	
	public void Build() {
		CalculateGoldPerDay();
		gameDate.DaysPassedEvent += DaysPassed;
	}
	
	float CalculateGoldPerDay() {
		return 0.35f;
	}
	
	void DaysPassed(int days) {
		floatGoldAccrued += goldPerDay * days;
		
		while(floatGoldAccrued >= 1.0f) {
			floatGoldAccrued -= 1.0f;
			realGoldAccrued += 1;
		}
	}

	public void ActivateBuilt() {
		inventory.Gold += realGoldAccrued;
		realGoldAccrued = 0;
	}

	public string DescribeUnbuilt() {
		return "Trade Post: Manages trade network between other towns.";
	}

	public string DescribeBuilt() {
		return "Trade Post: Collect " + realGoldAccrued + " gold.";
	}
}
