public class AnimationFactory {
	public static MapGraph mapGraph;

	public static MoveNearThenAttackAnimation CreateMoveNearThenAttackAnimation() {
		var anim = new MoveNearThenAttackAnimation();
		return anim;
	}

	public static AttackAnimation CreateAttackAnimation() {
		var anim = new AttackAnimation();
		return anim;
	}
}