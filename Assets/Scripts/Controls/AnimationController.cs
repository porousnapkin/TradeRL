using UnityEngine;

public static class AnimationController {
	public static void Move(GameObject characterArt, Vector2 gridDestination) {
		LeanTween.move(characterArt, Grid.GetCharacterWorldPositionFromGridPositon((int)gridDestination.x, (int)gridDestination.y), GlobalVariables.travelTime)
			.setEase(LeanTweenType.easeOutQuad);
	}

	public static void Attack(GameObject attackerArt, Character target, System.Action attackFinished, System.Action finishedMovingForward) {
		Vector3 startPosition = attackerArt.transform.position;
		Vector3 targetPos = Grid.GetCharacterWorldPositionFromGridPositon((int)target.WorldPosition.x, (int)target.WorldPosition.y);
	 	Vector3 dir = targetPos - attackerArt.transform.position;
		Vector3 endPosition = attackerArt.transform.position + (dir / 2);

		LeanTween.move(attackerArt, endPosition, GlobalVariables.attackTime).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
			AttackMoveBackwards(attackerArt, startPosition, attackFinished, finishedMovingForward));
	}

	static void AttackMoveBackwards(GameObject attackerArt, Vector2 startPosition, System.Action attackFinished, System.Action finishedMovingForward) {
		finishedMovingForward();
		LeanTween.move(attackerArt, startPosition, GlobalVariables.attackTime).setOnComplete(attackFinished);
	}
}