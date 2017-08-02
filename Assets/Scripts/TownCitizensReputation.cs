using System.Collections.Generic;
using UnityEngine;

public class TownCitizensReputation
{
    [Inject] public TownEventLog eventLog { private get; set; }

    int level = 0;
    Dictionary<int, List<TownBenefit>> levelToTownBenefits = new Dictionary<int, List<TownBenefit>>();
    int xp = 0;
    int xpToLevel = 100;
    const int baseXPToLevel = 80;

    public event System.Action OnXPChanged = delegate {};
    public event System.Action OnLevelChanged = delegate { };

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
        eventLog.AddTextEvent("Citizen reputation increased to " + level, "This towns citizens love you!");

        List<TownBenefit> benefits;
        if (levelToTownBenefits.TryGetValue(level, out benefits))
            benefits.ForEach(b => b.Apply());
        
        if (xp > xpToLevel)
            LevelUp();

        OnLevelChanged();
    }

    int CalculateXPToLevel()
    {
        return baseXPToLevel * (int)Mathf.Pow(3, level);
    }

    public void GainXP(int amount)
    {
        xp += amount;
        if (xp >= xpToLevel)
            LevelUp();

        OnXPChanged();
    }

    public int GetLevel()
    {
        return level;
    }

    public float GetPercentToNextLevel()
    {
        return (float)xp / (float)xpToLevel;
    }
}