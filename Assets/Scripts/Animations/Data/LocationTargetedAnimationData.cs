using UnityEngine;

public abstract class LocationTargetedAnimationData : ScriptableObject {
	public abstract LocationTargetedAnimation Create(Character owner);
}
