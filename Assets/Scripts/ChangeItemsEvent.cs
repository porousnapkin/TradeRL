public class ChangeItemsEvent : StoryActionEvent
{
    [Inject]public Inventory inventory { private get; set; }
    [Inject]public PlayerCharacter playerCharacter { private get; set; }
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
            var actualItem = item.Create(playerCharacter.GetCharacter());

            inventory.AddItem(actualItem);

            if(quantityChange > 1)
                actualItem.SetNumItems(quantityChange);
        }    
    }
}