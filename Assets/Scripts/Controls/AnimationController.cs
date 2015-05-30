using UnityEngine;

public static class AnimationController {
	public static void Move(GameObject characterArt, Vector2 gridDestination, System.Action moveFinished = null, float speedMod = 1.0f) {
		var worldDest = Grid.GetCharacterWorldPositionFromGridPositon((int)gridDestination.x, (int)gridDestination.y);
		CheckFacing(characterArt.transform.position, worldDest, characterArt);	

		var desc = LeanTween.move(characterArt, worldDest, GlobalVariables.travelTime * (1 / speedMod)).setEase(LeanTweenType.easeOutQuad);
		if(moveFinished != null)
			desc.setOnComplete(moveFinished);
	}

	static void CheckFacing(Vector3 start, Vector3 end, GameObject characterArt) {
		if((start - end).x > 0)
			characterArt.transform.localScale = new Vector3(-1, 1, 1);
		else
			characterArt.transform.localScale = new Vector3(1, 1, 1);
	}

	public static void Attack(GameObject attackerArt, Character attacker, Character target, System.Action attackFinished, System.Action finishedMovingForward) {
		CheckFacing(attacker.WorldPosition, target.WorldPosition, attackerArt);

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