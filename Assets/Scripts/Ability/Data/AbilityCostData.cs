using UnityEngine;

public abstract class AbilityCostData : ScriptableObject {
	public abstract AbilityCost Create(Character owner);
}

