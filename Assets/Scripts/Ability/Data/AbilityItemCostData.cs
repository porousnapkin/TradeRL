class AbilityItemCostData : AbilityCostData
{
    public ItemData item;

    public override AbilityCost Create(Character owner)
    {
        var cost = DesertContext.StrangeNew<AbilityItemCost>();
        cost.item = item;
        return cost;
    }
}