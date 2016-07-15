using UnityEngine;
using System.Collections;

public abstract class AbilityRestrictionData : ScriptableObject {
    public abstract AbilityRestriction Create(Character character);
}
