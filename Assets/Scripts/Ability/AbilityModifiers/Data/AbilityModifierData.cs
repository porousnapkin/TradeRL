using UnityEngine;

public abstract class AbilityModifierData : ScriptableObject
{
	public abstract AbilityModifier Create(CombatController owner);
}

