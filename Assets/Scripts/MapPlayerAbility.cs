using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class MapPlayerAbility : PlayerActivatedPower {
    public List<Restriction> restrictions { private get; set; }
    public List<Cost> costs { private get; set; }
    public string name;
    public MapAbilityActivator activator;

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
        //TODO:
        return "";
    }

    public void PayCosts()
    {
        costs.ForEach(c => c.PayCost());
    }

    public void RefundCosts()
    {
        costs.ForEach(c => c.Refund());
    }

    public void Activate(Action callback)
    {
        activator.Activate(callback);
    }

    public List<Cost> GetCosts()
    {
        return costs;
    }

    public List<Restriction> GetRestrictions()
    {
        return restrictions;
    }
}
