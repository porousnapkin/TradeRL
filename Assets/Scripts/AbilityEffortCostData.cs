using UnityEngine;
using System.Collections;
using System;

public class AbilityEffortCostData : AbilityCostData {
    public Effort.EffortType effortType;
    public int amount = 2;

    public override Cost Create(Character owner)
    {
        var cost = DesertContext.StrangeNew<EffortCost>();
        cost.amount = amount;
        cost.effortType = effortType;
        return cost;
    }
}
