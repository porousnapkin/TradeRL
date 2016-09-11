using UnityEngine;

public class AbilityItemCostDrawer : MonoBehaviour
{
    public ItemCost itemCost { private get; set; }
    MultiWrittenTextField text;
    int fieldIndex;

    void Start()
    {
        text = GetComponentInChildren<MultiWrittenTextField>();
        if(text == null)
            GameObject.Destroy(this);
        else
            fieldIndex = text.ReserveSpace();
    }

    void Update()
    {
        text.Write("Uses: " + itemCost.inventory.GetNumItemsByName(itemCost.item.itemName), fieldIndex);
    }
}