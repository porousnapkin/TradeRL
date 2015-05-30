using UnityEngine;

public class MoveNearThenAttackAnimation : LocationTargetedAnimation {
	public MapGraph mapGraph;
	public float moveSpeedMod = 2.0f;
	public Character owner;

	public void Play(Vector2 target, System.Action finished, System.Action activated) {
		AnimationController.Move(owner.ownerGO, owner.WorldPosition, () => MoveFinished(target, finished, activated), moveSpeedMod);
	}

	void MoveFinished(Vector2 location, System.Action finished, System.Action activated) {
		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			AnimationController.Attack(owner.ownerGO, owner, target, finished, activated);
	}
}