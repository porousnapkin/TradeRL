using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;
	public List<CityAction> cityActions = new List<CityAction>();
	public int maxGoodsDemanded = 20;
	public int demandedGoodsMet = 0;
	public int goodsDemanded { get { return maxGoodsDemanded - demandedGoodsMet; }}
	public int daysTillDemandReplenishes = 360;
	const int townStartingEconLevel = 0;
	const int cityStartingEconLevel = 1;
	public List<Town> rumoredLocations = new List<Town>();
	public List<Building> unbuiltBuilding = new List<Building>();
	public List<Building> builtBuilding = new List<Building>();
	int daysPassedForDemand = 0;
	int economicLevel = 0;
	int tradeXP = 0;

	public void Setup(GameDate gameDate, bool isCity) {
		economicLevel = isCity? cityStartingEconLevel : townStartingEconLevel;
		maxGoodsDemanded = MaxGoodsForEconomicLevel(economicLevel);

		gameDate.DaysPassedEvent += DaysPassed;
		GlobalEvents.GoodsSoldEvent += GoodsSold;
	}

	void DaysPassed (int days) {
		if(demandedGoodsMet > 0) {
			daysPassedForDemand += demandedGoodsMet;
			var daysToDemandRecovery = Mathf.FloorToInt (daysTillDemandReplenishes / maxGoodsDemanded);
			if(daysPassedForDemand > daysToDemandRecovery) {
				demandedGoodsMet--;
				daysPassedForDemand -= daysToDemandRecovery; 
			}
		}
	}
	
	void GoodsSold(int amount, TradeGood goods, Town locationSold) {
		if(locationSold != this || goods.locationPurchased == locationSold)
			return;

		demandedGoodsMet += amount;
		tradeXP += amount;
		if(tradeXP > maxGoodsDemanded)
			LevelUpEconomy();
	}

	int MaxGoodsForEconomicLevel(int level) {
		return Mathf.RoundToInt(10 * Mathf.Pow(2, level));
	}

	void LevelUpEconomy() {
		economicLevel++;
		tradeXP -= maxGoodsDemanded;
		maxGoodsDemanded = MaxGoodsForEconomicLevel(economicLevel);
		GlobalEvents.TownLeveldUpEvent(this);
	}
	
	public void AddPossibleBuliding(Building b) {
		unbuiltBuilding.Add(b);
	}
	
	public void BuildBuilding(Building b) {
		unbuiltBuilding.Remove (b);
		builtBuilding.Add (b);
	}
}