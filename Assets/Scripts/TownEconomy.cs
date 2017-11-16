using UnityEngine;
using System.Collections.Generic;

public class TownEconomy
{
    [Inject] public GameDate gameDate { private get; set; }
    [Inject] public TownEventLog eventLog { private get; set; }

    const int baseCostOfTradeGood = 20;
    const int basePaymentForForeignTradeGood = 40;
    public int CostOfTradeGood { get {
            var cost = baseCostOfTradeGood;
            tradeModifiers.ForEach(tm => cost += tm.GetCostOfTradeGoodAdjustment(cost));
            return cost;
        } }
    public int PaymentForForeignTradeGood { get {
            var cost = basePaymentForForeignTradeGood;
            tradeModifiers.ForEach(tm => cost += tm.GetPaymentForForeignGoodAdjustment(cost));
            return cost;
        } }
    public DailyReplenishingAsset goldForPurchasingGoods;
    public DailyReplenishingAsset goodsForSale;

    const int townStartingEconLevel = 1;
    const int cityStartingEconLevel = 1;
    const int baseXPToLevel = 10;
    int economicLevel = 0;
    int tradeXP = 0;
    List<TownTradeModifier> tradeModifiers = new List<TownTradeModifier>();
    HashSet<Town> townsWhoseGoodsHaveBeenSoldHere = new HashSet<Town>();
    Town town;

    public event System.Action<int> PlayerSoldForeignGoods = delegate { };
    public event System.Action<int> PlayerBoughtLocalGoods = delegate { };
    public event System.Action OnLevelChanged = delegate { };
    public event System.Action OnXPChanged = delegate { };
    public event System.Action OnTradeCostChanged = delegate { };

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
        AddXPForSoldGood(amount, goods);

        PlayerSoldForeignGoods(value);
    }

    void AddXPForSoldGood(int amount, TradeGood good)
    {
        if (townsWhoseGoodsHaveBeenSoldHere.Contains(good.locationPurchased))
            return;

        townsWhoseGoodsHaveBeenSoldHere.Add(good.locationPurchased);
        AddXP(baseXPToLevel * 2);
    }

    public void AddXP(int amount)
    {
        tradeXP += amount;
        CheckForLevelUp();

        OnXPChanged();
    }

    public int CalculatePriceTownPaysForGood(TradeGood tradeGood)
    {
        if (tradeGood.locationPurchased == town)
            return CostOfTradeGood;

        return PaymentForForeignTradeGood;
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
        return baseXPToLevel * economicLevel;
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

    public void AddTradeModifier(TownTradeModifier mod)
    {
        tradeModifiers.Add(mod);
        OnTradeCostChanged();
    }

    public void RemoveTradeModifier(TownTradeModifier mod)
    {
        tradeModifiers.Remove(mod);
        OnTradeCostChanged();
    }
}

