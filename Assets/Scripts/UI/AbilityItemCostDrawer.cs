using UnityEngine;

public class AbilityItemCostDrawer : AbilityButtonUseDrawer
{
    public MultiWrittenTextField text;
    int fieldIndex;

    void Awake()
    {
        fieldIndex = text.ReserveSpace();
    }

    public override void CheckCost(AbilityCost cost)
    {
        var itemCost = cost as AbilityItemCost;
        if (itemCost == null)
            return;

        text.Write("Uses: " + itemCost.inventory.GetNumItemsByName(itemCost.item.itemName), fieldIndex);
    }

    public override void CheckRestriction(Restriction restriction)
    {
    }
}