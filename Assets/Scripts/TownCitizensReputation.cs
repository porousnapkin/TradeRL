using System;
using System.Collections.Generic;
using UnityEngine;

public class TownCitizensReputation
{
    int level = 0;
    Dictionary<int, List<TownBenefit>> levelToTownBenefits = new Dictionary<int, List<TownBenefit>>();
    int xp = 0;
    int xpToLevel = 100;
    const int baseXPToLevel = 100;

    public void Setup(TownEconomy economy)
    {
        xpToLevel = CalculateXPToLevel();
        economy.PlayerBoughtLocalGoods += GainXP;
        economy.PlayerSoldForeignGoods += GainXP;
    }

    public void AddLevelBenefit(int level, TownBenefit townBenefit)
    {
        List<TownBenefit> benefits;
        if (levelToTownBenefits.TryGetValue(level, out benefits))
            benefits.Add(townBenefit);
        else
            levelToTownBenefits[level] = new List<TownBenefit>(new TownBenefit[1] { townBenefit });
    }

    void LevelUp()
    {
        level++;
        xp -= xpToLevel;
        xpToLevel = CalculateXPToLevel();

        List<TownBenefit> benefits;
        if (levelToTownBenefits.TryGetValue(level, out benefits))
            benefits.ForEach(b => b.Apply());
        
        if (xp > xpToLevel)
            LevelUp();
    }

    int CalculateXPToLevel()
    {
        return baseXPToLevel * (int)Mathf.Pow(2, level);
    }

    public void GainXP(int amount)
    {
        xp += amount;
        Debug.Log("citizen rep: " + xp + "/" + xpToLevel);
        if (xp >= xpToLevel)
            LevelUp();
    }
}