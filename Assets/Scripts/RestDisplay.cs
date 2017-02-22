using System.Collections.Generic;

public class RestDisplay : CityActionDisplay
{
    public float daysPerHP = 1;
    public int goldPerDay = 1;
    public int effortPerDay = 5;
    public List<CityRestButton> restButtons;

    protected override void Start()
    {
        base.Start();

        restButtons.ForEach(b => b.SetData(daysPerHP, goldPerDay, effortPerDay));
    }
}