using UnityEngine;

public class TownEconomy
{
    [Inject] public GameDate gameDate { private get; set; }
    [Inject] public TownEventLog eventLog { private get; set; }

    public int CostOfTradeGood { get { return 20; } }
    public DailyReplenishingAsset goldForPurchasingGoods;
    public DailyReplenishingAsset goodsForSale;

    const int townStartingEconLevel = 1;
    const int cityStartingEconLevel = 2;

    int economicLevel = 0;
    int tradeXP = 0;

    Town town;

    public event System.Action<int> PlayerSoldForeignGoods = delegate { };
    public event System.Action<int> PlayerBoughtLocalGoods = delegate { };
    public event System.Action OnLevelChanged = delegate { };
    public event System.Action OnXPChanged = delegate { };

    public void Setup(bool isCity, Town town)
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
        if (goods.locationPurchased == locationSold || locationSold != town)
            return;

        var value = amount * CalculatePriceTownPaysForGood(goods);
        goldForPurchasingGoods.Spend(value);
        AddXP(amount);

        PlayerSoldForeignGoods(value);
    }

    public void AddXP(int amount)
    {
        tradeXP += amount;
        CheckForLevelUp();

        OnXPChanged();
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
        AddXP(amount);

        PlayerBoughtLocalGoods(amount * CostOfTradeGood);
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
        eventLog.AddTextEvent("Economy increased to level " + economicLevel, "This increases the number of trade goods\nthat can be bought and sold here.");

		goodsForSale.Max = MaxGoodsForEconomicLevel(economicLevel);
		goldForPurchasingGoods.Max = MaxGoodsForEconomicLevel(economicLevel) * CostOfTradeGood;

		GlobalEvents.TownEconomyLeveldUpEvent(town);
        OnLevelChanged();
    }

    public int GetLevel()
    {
        return economicLevel;
    }

    public float GetPercentToNextLevel()
    {
        return (float)tradeXP / (float)GetEconXPForLevel();
    }
}

