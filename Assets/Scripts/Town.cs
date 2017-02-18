using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;
	public List<CityActionData> cityActions = new List<CityActionData>();
	int maxGoodsDemanded = 20;
	public int MaxGoodsDemanded { get { return maxGoodsDemanded; }}
	int demandedGoodsMet = 0;
	int maxGoodsSurplus = 20;
	public int MaxGoodsSurplus { get { return maxGoodsSurplus; }}
	int goodsPurchased = 0;
	public int supplyGoods { get { return maxGoodsSurplus - goodsPurchased; }}
	public int goodsDemanded { get { return maxGoodsDemanded - demandedGoodsMet; }}
	int daysTillDemandReplenishes = 150;
	const int townStartingEconLevel = 0;
	const int cityStartingEconLevel = 1;
    public List<ItemData> travelSuppliesAvailable = new List<ItemData>();
	public List<Town> rumoredLocations = new List<Town>();
	public List<Building> unbuiltBuilding = new List<Building>();
	public List<Building> builtBuilding = new List<Building>();
	int daysPassedForDemand = 0;
	int economicLevel = 0;
	public int EconomicLevel { get { return economicLevel; }}
	int tradeXP = 0;
	public event System.Action<Town> economyUpdated = delegate{};
	public event System.Action<Town, Building> possibleBuildingGainedEvent = delegate{};
	public event System.Action<Town, CityActionData> cityActionAddedEvent = delegate{};

	public void Setup(GameDate gameDate, bool isCity) {
		economicLevel = isCity? cityStartingEconLevel : townStartingEconLevel;
		maxGoodsDemanded = MaxGoodsForEconomicLevel(economicLevel);
		maxGoodsSurplus = MaxGoodsForEconomicLevel(economicLevel);

		gameDate.DaysPassedEvent += DaysPassed;
		GlobalEvents.GoodsSoldEvent += GoodsSold;
		GlobalEvents.GoodsPurchasedEvent += GoodsPurchased;
	}

	void DaysPassed (int days) {
		if(demandedGoodsMet > 0) {
			daysPassedForDemand += days;
			var daysToDemandRecovery = Mathf.FloorToInt (daysTillDemandReplenishes / maxGoodsDemanded);
			if(daysPassedForDemand > daysToDemandRecovery) {
				demandedGoodsMet--;
				goodsPurchased--;
				daysPassedForDemand -= daysToDemandRecovery; 
			}
		}
	}
	
	void GoodsSold(int amount, TradeGood goods, Town locationSold) {
		if(locationSold != this || goods.locationPurchased == locationSold)
			return;

		demandedGoodsMet += amount;
		tradeXP += amount;
		CheckForLevelUp();
	}

	void GoodsPurchased(int amount, Town wherePurchased) {
		if(this != wherePurchased)
			return;

		goodsPurchased += amount;
		tradeXP += amount;
		CheckForLevelUp();
	}

	void CheckForLevelUp() {
		if(tradeXP > maxGoodsDemanded * 2)
			LevelUpEconomy();
	}

	int MaxGoodsForEconomicLevel(int level) {
		return Mathf.RoundToInt(10 * Mathf.Pow(2, level));
	}

	void LevelUpEconomy() {
		economicLevel++;
		Debug.Log ("Economy Leveled up to level " + economicLevel);
		tradeXP -= maxGoodsDemanded;
		int modifiedMaxGoods = MaxGoodsForEconomicLevel(economicLevel) - maxGoodsSurplus;
		maxGoodsSurplus = MaxGoodsForEconomicLevel(economicLevel);
		maxGoodsDemanded = MaxGoodsForEconomicLevel(economicLevel);
		goodsPurchased += modifiedMaxGoods;
		demandedGoodsMet += modifiedMaxGoods;
		economyUpdated(this);
		GlobalEvents.TownLeveldUpEvent(this);
	}
	
	public void AddPossibleBuliding(Building b) {
		unbuiltBuilding.Add(b);
		possibleBuildingGainedEvent(this, b);
		CheckForBuildingScene();
	}

	void CheckForBuildingScene() {
		if(cityActions.Find(a => a.name == "Building") == null &&
		   (unbuiltBuilding.Count > 0 || 
		 	builtBuilding.Count > 0))
			AddCityAction(Resources.Load ("CityActions/Building") as CityActionData);
	}
	
	public void BuildBuilding(Building b) {
		unbuiltBuilding.Remove (b);
		builtBuilding.Add (b);
		CheckForBuildingScene();
	}

	public void AddCityAction(CityActionData ca) {
		cityActions.Add (ca);
		cityActionAddedEvent(this, ca);
	}
}