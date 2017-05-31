using UnityEngine;
using System.Collections.Generic;

public class Town {
	public Vector2 worldPosition;
	public string name;
	public List<CityActionData> cityActions = new List<CityActionData>();

    int maxGoldForGoods = 500;
    int goldSpentForGoods = 0;
	public int goldAvailableForGoods { get { return maxGoldForGoods - goldSpentForGoods; }}
    public int costOfTradeGood { get { return 20; } }
	float goldReplenishedPerDay = 1;
    float goldProductionRunoff = 0;

	int maxGoodsSurplus = 20;
	public int MaxGoodsSurplus { get { return maxGoodsSurplus; }}
	int goodsPurchased = 0;
    float tradeGoodsProducedPerDay = 0.05f;
    float tradeGoodProductionRunoff = 0;

	public int supplyGoods { get { return maxGoodsSurplus - goodsPurchased; }}
	const int townStartingEconLevel = 1;
	const int cityStartingEconLevel = 2;
    public List<ItemData> travelSuppliesAvailable = new List<ItemData>();
    public List<HireableAllyData> hireableAllies = new List<HireableAllyData>();
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

        maxGoldForGoods = MaxGoodsForEconomicLevel(economicLevel) * costOfTradeGood;
		maxGoodsSurplus = MaxGoodsForEconomicLevel(economicLevel);

		gameDate.DaysPassedEvent += DaysPassed;
		GlobalEvents.GoodsSoldEvent += PlayerSoldGoods;
		GlobalEvents.GoodsPurchasedEvent += PlayerPurchasedGoods;
	}

	void DaysPassed (int days) {
        var goldProduced = GenerateAssetOverTime(days, ref goldReplenishedPerDay, ref goldProductionRunoff);
        goldSpentForGoods = Mathf.Max(0, goldSpentForGoods - goldProduced);

        var goodsProduced = GenerateAssetOverTime(days, ref tradeGoodsProducedPerDay, ref tradeGoodProductionRunoff);
        goodsPurchased = Mathf.Max(0, goodsPurchased - goodsProduced);
	}

    int GenerateAssetOverTime(int days, ref float producedPerDay, ref float runoff)
    {
        var produced = runoff + (producedPerDay * days);
        var intPart = Mathf.FloorToInt(produced);
        runoff = produced - intPart;
        return intPart;
    }

    public int CalculatePriceTownPaysForGood(TradeGood tradeGood)
    {
        //TODO: figure this out...
        if (tradeGood.locationPurchased == this)
            return costOfTradeGood;

        var distance = Vector2.Distance(tradeGood.locationPurchased.worldPosition, worldPosition);
        return 40 + Mathf.RoundToInt(Mathf.Max(distance - 15, 0));
    }

    void PlayerSoldGoods(int amount, TradeGood goods, Town locationSold) {
		if(locationSold != this || goods.locationPurchased == locationSold)
			return;

        goldSpentForGoods += amount * CalculatePriceTownPaysForGood(goods);
		tradeXP += amount;
		CheckForLevelUp();
	}

	void PlayerPurchasedGoods(int amount, Town wherePurchased) {
		if(this != wherePurchased)
			return;

		goodsPurchased += amount;
		tradeXP += amount;
		CheckForLevelUp();
	}

	void CheckForLevelUp() {
		if(tradeXP > GetEconXPForLevel())
			LevelUpEconomy();
	}

    int GetEconXPForLevel() {
        return 10 * economicLevel;
    }

	int MaxGoodsForEconomicLevel(int level) {
		return Mathf.RoundToInt(10 * Mathf.Pow(2, level));
	}

	void LevelUpEconomy() {
		tradeXP -= GetEconXPForLevel();
		economicLevel++;
		Debug.Log ("Economy Leveled up to level " + economicLevel);
		int modifiedMaxGoods = MaxGoodsForEconomicLevel(economicLevel) - maxGoodsSurplus;
		maxGoodsSurplus = MaxGoodsForEconomicLevel(economicLevel);
		maxGoldForGoods = MaxGoodsForEconomicLevel(economicLevel) * costOfTradeGood;
		goodsPurchased += modifiedMaxGoods;
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