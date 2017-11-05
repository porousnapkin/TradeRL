using System.Collections.Generic;
using UnityEngine;

public class TownCitizensReputation
{
    [Inject] public TownEventLog eventLog { private get; set; }
    [Inject] public TownUpgradeTracks upgradeTracks { get; set; }

    int level = 0;
    int xp = 0;
    int xpToLevel = 100;
    const int baseXPToLevel = 80;
    Town town;

    public event System.Action OnXPChanged = delegate {};
    public event System.Action OnLevelChanged = delegate { };

    public void Setup(Town town, TownEconomy economy)
    {
        this.town = town;

        xpToLevel = CalculateXPToLevel();
        economy.PlayerBoughtLocalGoods += GainXP;
        economy.PlayerSoldForeignGoods += GainXP;
    }

    public void SetupUpgradeTracks(List<TownData.ListOfTownUpgradeOptions> tracks)
    {
        upgradeTracks.Setup(tracks);
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

    void LevelUp()
    {
        level++;
        xp -= xpToLevel;
        xpToLevel = CalculateXPToLevel();
        eventLog.AddTextEvent("Citizen reputation increased to " + level, "This towns citizens love you!");

        if (xp > xpToLevel)
            LevelUp();

        OnLevelChanged();
    }

    int CalculateXPToLevel()
    {
        return baseXPToLevel * (int)Mathf.Pow(3, level);
    }

    public float GetPercentToNextLevel()
    {
        return (float)xp / (float)xpToLevel;
    }
}