public class AttackAnimationData : LocationTargetedAnimationData  {
	public override LocationTargetedAnimation Create(Character owner) {
		var anim = AnimationFactory.CreateAttackAnimation();
		anim.owner = owner;
		return anim;
	}
}
