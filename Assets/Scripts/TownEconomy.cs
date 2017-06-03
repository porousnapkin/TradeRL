using UnityEngine;

public class TownEconomy
{
    public int CostOfTradeGood { get { return 20; } }
    public DailyReplenishingAsset goldForPurchasingGoods;
    public DailyReplenishingAsset goodsForSale;

	const int townStartingEconLevel = 1;
	const int cityStartingEconLevel = 2;

    int economicLevel = 0;
	public int EconomicLevel { get { return economicLevel; }}
	int tradeXP = 0;

    Town town;

    public void Setup(GameDate gameDate, bool isCity, Town town)
    {
        this.town = town;

        economicLevel = isCity ? cityStartingEconLevel : townStartingEconLevel;

        goldForPurchasingGoods = new DailyReplenishingAsset(MaxGoodsForEconomicLevel(economicLevel) * CostOfTradeGood, 1, gameDate);
        goodsForSale = new DailyReplenishingAsset(MaxGoodsForEconomicLevel(economicLevel), 0.05f, gameDate);

        GlobalEvents.GoodsSoldEvent += PlayerSoldGoods;
        GlobalEvents.GoodsPurchasedEvent += PlayerPurchasedGoods;
    }

    void PlayerSoldGoods(int amount, TradeGood goods, Town locationSold)
    {
        if (goods.locationPurchased == locationSold)
            return;

        goldForPurchasingGoods.Spend(amount * CalculatePriceTownPaysForGood(goods));
        tradeXP += amount;
        CheckForLevelUp();
    }

    public int CalculatePriceTownPaysForGood(TradeGood tradeGood)
    {
        //TODO: figure this out...
        if (tradeGood.locationPurchased == town)
            return CostOfTradeGood;

        var distance = Vector2.Distance(tradeGood.locationPurchased.worldPosition, town.worldPosition);
        return 40 + Mathf.RoundToInt(Mathf.Max(distance - 15, 0));
    }

    void PlayerPurchasedGoods(int amount, Town wherePurchased) {
		if(town != wherePurchased)
			return;

        goodsForSale.Spend(amount);
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

		goodsForSale.Max = MaxGoodsForEconomicLevel(economicLevel);
		goldForPurchasingGoods.Max = MaxGoodsForEconomicLevel(economicLevel) * CostOfTradeGood;

		GlobalEvents.TownEconomyLeveldUpEvent(town);
	}
}

