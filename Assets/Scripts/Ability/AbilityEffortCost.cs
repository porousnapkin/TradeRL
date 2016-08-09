using System;

public class AbilityEffortCost : AbilityCost {
    [Inject]public Effort effort { private get; set; }
    public Effort.EffortType effortType { private get; set; }
    public int amount { private get; set; }

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
}
