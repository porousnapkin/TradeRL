using System;
using UnityEngine;

public class DaysEffectDuration : EffectDuration
{
    public enum CombineType
    {
        Add,
        Highest,
        Nothing,
    }
    public CombineType combineType { get; set; }
    [Inject] public GameDate gameDate { get; set; }
    public int days { get; set; }
    int daysPassed = 0;

    public event Action Finished = delegate {};

    public void Apply()
    {
        gameDate.DaysPassedEvent += DaysPassed;
    }

    private void DaysPassed(int days)
    {
        daysPassed += days;
        if (daysPassed >= days)
            Finished();
    }

    public void CombineWith(EffectDuration duration)
    {
        var other = duration as DaysEffectDuration;
        if (other == null)
            return;

        switch (combineType)
        {
            case CombineType.Add:
                days += other.days;
                break;
            case CombineType.Highest:
                days = Mathf.Max(other.days, days);
                break;
            case CombineType.Nothing:
                break;
        }
    }
}