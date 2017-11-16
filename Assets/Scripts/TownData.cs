using System.Collections.Generic;
using UnityEngine;

public class TownData: ScriptableObject
{
    public string displayName = "Da Town";

    [System.Serializable]
    public class ListOfTownUpgradeOptions
    {
        public List<TownUpgradeOptionData> list = new List<TownUpgradeOptionData>();
    }

    public List<ListOfTownUpgradeOptions> politicalUpgradeOptions = new List<ListOfTownUpgradeOptions>();
    public List<ListOfTownUpgradeOptions> citizenUpgradeOptions = new List<ListOfTownUpgradeOptions>();
    public int baseCitizenXPPerLevel = 80;
    public int basePoliticalXPPerLevel = 80;

    public Town Create(Vector2 location)
    {
        var t = DesertContext.StrangeNew<Town>();
        t.worldPosition = location;
        t.name = displayName;
        t.Setup(true);

        t.citizensReputation.SetupUpgradeTracks(citizenUpgradeOptions);
        t.citizensReputation.BaseXPToLevel = baseCitizenXPPerLevel;
        t.politicalReputation.SetupUpgradeTracks(politicalUpgradeOptions);
        t.politicalReputation.BaseXPToLevel = basePoliticalXPPerLevel;

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