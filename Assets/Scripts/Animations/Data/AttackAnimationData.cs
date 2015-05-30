public class AttackAnimationData : LocationTargetedAnimationData  {
	public override LocationTargetedAnimation Create() {
		return AnimationFactory.CreateAttackAnimation();
	}
}
