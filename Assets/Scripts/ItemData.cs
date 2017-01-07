using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemEffectData effect;
    public bool canJam = false;
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