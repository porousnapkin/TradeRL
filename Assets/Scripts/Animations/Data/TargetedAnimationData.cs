using UnityEngine;

public abstract class TargetedAnimationData : ScriptableObject {
	public abstract TargetedAnimation Create(Character owner);
}
