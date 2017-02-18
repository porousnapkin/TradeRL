using System.Collections.Generic;
using UnityEngine;

public class CityBasics : ScriptableObject
{
    public static CityBasics Instance
    {
        get
        {
            return Resources.Load("CityBasics") as CityBasics;
        }
    }

    public List<CityActionData> defaultCityActivities;
    public List<ItemData> defaultTravelSupplies;
}

