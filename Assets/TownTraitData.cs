﻿using System.Collections.Generic;
using UnityEngine;

public class TownTraitData : ScriptableObject
{
    public List<CityActionData> cityActivities = new List<CityActionData>();
    public List<ItemData> travelSupplies = new List<ItemData>();
    public List<HireableAllyData> hireableAllies = new List<HireableAllyData>();

    public List<TownBenefitData> benefitsForCitizenReputation = new List<TownBenefitData>();

    public void Apply(Town t)
    {
        cityActivities.ForEach(a => t.playerActions.AddAction(a));
        t.travelSuppliesAvailable.AddRange(travelSupplies);
        t.hireableAllies.AddRange(hireableAllies);

        for(int i = 0; i < benefitsForCitizenReputation.Count; i++) 
            t.citizensReputation.AddLevelBenefit(i+1, benefitsForCitizenReputation[i].Create(t));
    }
}