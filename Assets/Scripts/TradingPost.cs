﻿using UnityEngine;
using System.Collections;

public class TradingPost : BuildingAbility {
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public Inventory inventory { private get; set;}
	float goldPerDay = 5.0f;
	float floatGoldAccrued = 0;
	int realGoldAccrued = 0;
	
	public void Build() {
		CalculateGoldPerDay();
		gameDate.DaysPassedEvent += DaysPassed;
	}
	
	float CalculateGoldPerDay() {
		return 1.0f;
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
