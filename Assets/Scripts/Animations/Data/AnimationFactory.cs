public class AnimationFactory {
	public static MapGraph mapGraph;

	public static MoveNearThenAttackAnimation CreateMoveNearThenAttackAnimation() {
		var anim = new MoveNearThenAttackAnimation();
		anim.mapGraph = mapGraph;
		return anim;
	}

	public static AttackAnimation CreateAttackAnimation() {
		var anim = new AttackAnimation();
		anim.mapGraph = mapGraph;
		return anim;
	}
}