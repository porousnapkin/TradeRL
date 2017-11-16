using UnityEngine;

public abstract class EffectActionData : ScriptableObject
{
    public abstract EffectAction Create(Character character);
}

