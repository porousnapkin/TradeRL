using UnityEngine;
using System.Collections;
using System;

public class AbilityEffortCostData : AbilityCostData {
    public Effort.EffortType effortType;
    public int amount = 2;

    public override AbilityCost Create(Character owner)
    {
        var cost = DesertContext.StrangeNew<AbilityEffortCost>();
        cost.amount = amount;
        cost.effortType = effortType;
        return cost;
    }
}
