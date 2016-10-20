using UnityEngine;

public abstract class ItemEffectData : ScriptableObject
{
    public abstract ItemEffect Create(Character character);
}

