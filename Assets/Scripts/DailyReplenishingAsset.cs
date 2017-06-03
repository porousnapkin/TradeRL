using UnityEngine;

public class DailyReplenishingAsset
{
    public int Max {
        get
        {
            return max;
        }
        set
        {
            max = value;
        }
    }

    int max = 500;
    int amountSpent = 0;
    public int Available { get { return max - amountSpent; } }
    float replenishedPerDay = 1;
    float productionRunoff = 0;

    public DailyReplenishingAsset(int max, float replenishedPerDay, GameDate gameDate)
    {
        this.max = max;
        this.replenishedPerDay = replenishedPerDay;

        gameDate.DaysPassedEvent += DaysPassed;
    }

    void DaysPassed(int days)
    {
        var produced = GenerateAssetOverTime(days);
        amountSpent = Mathf.Max(0, amountSpent - produced);
    }

    int GenerateAssetOverTime(int days)
    {
        var produced = productionRunoff + (replenishedPerDay * days);
        var intPart = Mathf.FloorToInt(produced);
        productionRunoff = produced - intPart;
        return intPart;
    }

    public void Spend(int amount)
    {
        amountSpent += amount;
    }
}
