using System.Collections.Generic;
using UnityEngine;

public class Reputation
{
    [Inject] public TownEventLog eventLog { private get; set; }
    [Inject] public TownUpgradeTracks upgradeTracks { get; set; }

    int level = 0;
    int xp = 0;
    int xpToLevel = 100;
    int baseXPToLevel = 80;
    Town town;

    public event System.Action OnXPChanged = delegate {};
    public event System.Action OnLevelChanged = delegate { };

    public int BaseXPToLevel
    {
        get { return baseXPToLevel; }
        set
        {
            baseXPToLevel = value;
            xpToLevel = CalculateXPToLevel();
        }
    }

    public void Setup(Town town)
    {
        this.town = town;

        xpToLevel = CalculateXPToLevel();
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