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
		CheckFacing(attacker.Position, target.Position, attackerArt);

		Vector3 startPosition = attackerArt.transform.position;
        Vector3 dir = Grid.Get1YMove() * (attacker.myFaction == Faction.Enemy ? 1 : -1);
        dir.z = 0;
		Vector3 endPosition = attackerArt.transform.position + (dir / 2);

		LeanTween.move(attackerArt, endPosition, GlobalVariables.attackTime).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
			AttackMoveBackwards(attackerArt, startPosition, attackFinished, finishedMovingForward));
	}

	static void AttackMoveBackwards(GameObject attackerArt, Vector2 startPosition, System.Action attackFinished, System.Action finishedMovingForward) {
		finishedMovingForward();
		LeanTween.move(attackerArt, startPosition, GlobalVariables.attackTime).setOnComplete(attackFinished);
	}

	public static void Damaged(GameObject target) {
		var sr = target.GetComponent<SpriteRenderer>();
		Color original = Color.white;
		LeanTween.value(target, val => sr.color = new Color(original.r * (0.75f + val / 4), original.g * val, original.b * val), 0, 1, 0.35f).
			setEase(LeanTweenType.easeOutQuart);
	}

	public static void Die(GameObject target, System.Action finished) {
		var sr = target.GetComponent<SpriteRenderer>();
		Color original = Color.white;
		LeanTween.value(target, val => sr.color = new Color(original.r, original.g, original.b, val), 1, 0, 0.5f).setOnComplete(finished);
	}
}