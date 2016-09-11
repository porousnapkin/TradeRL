class ChangeItemsEvent : StoryActionEvent
{
    [Inject]public Inventory inventory { private get; set; }
    public ItemData item;
    public int quantityChange = 1;

    public void Activate()
    {
        var invItem = inventory.GetItemByName(item.itemName);
        if (invItem != null)
        {
            invItem.SetNumItems(invItem.GetNumItems() + quantityChange);
        }
        else if(quantityChange > 0)
        {
            var actualItem = item.Create(null);
            actualItem.SetNumItems(quantityChange);

            inventory.AddItem(actualItem);
        }    
    }
}