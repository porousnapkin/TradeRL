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
    public int DaysTillReplenished { get { return daysToReplenish - daysReplenishing; } }
    public bool IsReplenishing { get { return amountSpent > 0; } }
    int daysToReplenish = 120;
    int daysReplenishing = 0;
    public event System.Action goodsPurchasedEvent = delegate { };

    public DailyReplenishingAsset(int max, int daysToReplenish, GameDate gameDate)
    {
        this.max = max;
        this.daysToReplenish = daysToReplenish;

        gameDate.DaysPassedEvent += DaysPassed;
    }

    void DaysPassed(int days)
    {
        if (!IsReplenishing)
            return;

        daysReplenishing++;
        if(daysReplenishing >= daysToReplenish)
        {
            amountSpent = 0;
            daysReplenishing = 0;
        }
    }

    public void Spend(int amount)
    {
        amountSpent += amount;
        goodsPurchasedEvent();
    }
}
