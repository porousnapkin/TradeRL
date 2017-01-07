using UnityEngine;

public class ItemIsUnjammedRestriction : Restriction
{
    [Inject]public Inventory inventory { private get; set; }
    public ItemData item { private get; set; }

    public bool CanUse()
    {
        var actualItem = inventory.GetItemByName(item.itemName);
        return !actualItem.IsJammed();
    }

    public void SetupVisualization(GameObject go)
    {
        var actualItem = inventory.GetItemByName(item.itemName);
        var drawer = go.AddComponent<JamChanceDrawer>();
        drawer.item = actualItem;
    }
}

