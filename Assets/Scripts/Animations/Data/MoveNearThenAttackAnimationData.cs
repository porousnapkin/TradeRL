using UnityEngine;

public class MoveNearThenAttackAnimationData : LocationTargetedAnimationData  {
	public float moveSpeedMultiplier = 2.0f;
	public override LocationTargetedAnimation Create(Character owner) {
		var anim = AnimationFactory.CreateMoveNearThenAttackAnimation();
		anim.moveSpeedMod = moveSpeedMultiplier;
		anim.owner = owner;
		return anim;
	}
}
