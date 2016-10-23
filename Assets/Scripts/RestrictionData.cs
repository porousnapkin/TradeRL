using UnityEngine;

public abstract class RestrictionData : ScriptableObject {
    public abstract Restriction Create(Character character);
}

