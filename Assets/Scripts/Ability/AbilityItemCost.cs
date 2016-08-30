class AbilityItemCost : AbilityCost
{
    [Inject] public Inventory inventory { private get; set; }
    public ItemData item { private get; set; }

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
}