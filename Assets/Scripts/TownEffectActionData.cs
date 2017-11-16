using UnityEngine;

public abstract class TownEffectActionData : ScriptableObject
{
    public abstract EffectAction Create(Town t);
}

