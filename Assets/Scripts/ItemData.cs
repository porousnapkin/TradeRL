using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemEffectData effect;
    public bool canJam = false;
    public int standardPurchasePrice = 20;
    public float jamChance = 0.2f;

    public Item Create(Character character)
    {
        var item = DesertContext.StrangeNew<Item>();
        item.name = itemName;
        item.jamChance = jamChance;
        item.effect = effect.Create(character);
        return item;
    }
}