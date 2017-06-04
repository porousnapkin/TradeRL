using System.Collections.Generic;
using UnityEngine;

public class TownData: ScriptableObject
{
    public string displayName = "Da Town";

    public List<TownTraitData> traits = new List<TownTraitData>();

    public Town Create(Vector2 location)
    {
        var t = DesertContext.StrangeNew<Town>();
        t.worldPosition = location;
        t.name = displayName;
        t.Setup(true);

        traits.ForEach(trait => trait.Apply(t));

        return t;
    }

    //TODO: need something like this?
    //void SetupBasics(Town t)
    //{
    //    var basics = CityBasics.Instance;
    //    basics.defaultCityActivities.ForEach(a => t.playerActions.AddAction(a));
    //    basics.defaultTravelSupplies.ForEach(s => t.travelSuppliesAvailable.Add(s));
    //    basics.hireableAllies.ForEach(s => t.hireableAllies.Add(s));
    //}
}