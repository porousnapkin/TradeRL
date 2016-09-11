using System;
using UnityEngine;

public class ItemCost : Cost
{
    [Inject] public Inventory inventory { get; set; }
    public ItemData item { get; set; }

    public bool CanAfford()
    {
        return inventory.GetNumItemsByName(item.itemName) > 0;
    }

    public void PayCost()
    {
        var actualItem = inventory.GetItemByName(item.itemName);
        if(actualItem !=  null)
            actualItem.SetNumItems(actualItem.GetNumItems() - 1);
    }

    public void Refund()
    {
        var actualItem = inventory.GetItemByName(item.itemName);
        if(actualItem !=  null)
            actualItem.SetNumItems(actualItem.GetNumItems() + 1);
    }

    public void SetupVisualization(GameObject go)
    {
        var drawer = go.AddComponent<AbilityItemCostDrawer>();
        drawer.itemCost = this;
    }
}