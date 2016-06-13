using UnityEngine;

public abstract class AbilityActivatorData : ScriptableObject {
	public abstract AbilityActivator Create(CombatController owner);
}