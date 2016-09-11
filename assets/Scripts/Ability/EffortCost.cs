using System;
using UnityEngine;

public class EffortCost : Cost {
    [Inject]public Effort effort { private get; set; }
    public Effort.EffortType effortType { get; set; }
    public int amount { get; set; }

    public bool CanAfford()
    {
        return effort.GetEffort(effortType) >= amount;
    }

    public void PayCost()
    {
        effort.SetEffort(effortType, effort.GetEffort(effortType) - amount);
    }

    public void Refund()
    {
        effort.SetEffort(effortType, effort.GetEffort(effortType) + amount);
    }

    public void SetupVisualization(GameObject go)
    {
        var drawer = go.AddComponent<EffortCostDrawer>();
        drawer.cost = this;
    }
}
