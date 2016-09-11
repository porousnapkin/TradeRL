using UnityEngine;

public abstract class InputTargetFilterData : ScriptableObject {
	public abstract InputTargetFilter Create(Character owner);
}