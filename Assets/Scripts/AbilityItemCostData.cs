public class AbilityItemCostData : AbilityCostData
{
    public ItemData item;

    public override Cost Create(Character owner)
    {
        var cost = DesertContext.StrangeNew<ItemCost>();
        cost.item = item;
        return cost;
    }
}