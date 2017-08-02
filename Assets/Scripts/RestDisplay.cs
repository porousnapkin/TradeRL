using System.Collections.Generic;

public class RestDisplay : CityActionDisplay
{
    public List<CityRestButton> restButtons;

    protected override void Start()
    {
        base.Start();

        restButtons.ForEach(b => b.SetData());
    }
}