using UnityEngine;

public class MoveNearThenAttackAnimationData : LocationTargetedAnimationData  {
	public float moveSpeedMultiplier = 2.0f;
	public override LocationTargetedAnimation Create() {
		var anim = AnimationFactory.CreateMoveNearThenAttackAnimation();
		anim.moveSpeedMod = moveSpeedMultiplier;
		return anim;
	}
}
