using System;
using System.Collections.Generic;
using System.Linq;

public class MapPlayerAbility : PlayerActivatedPower {
    public List<Restriction> restrictions { private get; set; }
    public List<Cost> costs { private get; set; }
    public string name;
    public MapAbilityActivator activator;
    public string description;

    public int TurnsRemainingOnCooldown
    {
        get { return 0; }
    }

    public bool CanUse()
    {
        return costs.All(c => c.CanAfford()) && restrictions.All(r => r.CanUse());
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }

    public void PrePurchase()
    {
        costs.ForEach(c => c.PayCost());
    }

    public void RefundUse()
    {
        costs.ForEach(c => c.Refund());
    }

    public void Activate(Action callback)
    {
        activator.Activate(callback);
    }

    public List<Visualizer> GetVisualizers()
    {
        var l = new List<Visualizer>();
        l.AddRange(GetVisualizersFromList(costs));
        l.AddRange(GetVisualizersFromList(restrictions));
        return l;
    }

    List<Visualizer> GetVisualizersFromList<T>(List<T> list)
    {
        var newList = list.ConvertAll(x => x as Visualizer);
        newList.RemoveAll(x => x == null);
        return newList;
    }
}
