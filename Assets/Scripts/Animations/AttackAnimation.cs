using UnityEngine;

public class AttackAnimation : LocationTargetedAnimation {
	public MapGraph mapGraph;
	public Character owner;
	public void Play(Vector2 location, System.Action finished, System.Action activated) {
		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			AnimationController.Attack(owner.ownerGO, owner, target, finished, activated);
	}
}