public class AttackAnimationData : TargetedAnimationData  {
	public override TargetedAnimation Create(Character owner) {
		var anim = AnimationFactory.CreateAttackAnimation();
		anim.owner = owner;
		return anim;
	}
}