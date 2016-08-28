using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemEffectData effect;
    public int defaultNum = 1;

    public Item Create(Character character)
    {
        var item = DesertContext.StrangeNew<Item>();
        item.name = itemName;
        item.effect = effect.Create(character);
        item.SetNumItems(defaultNum);
        return item;
    }
}