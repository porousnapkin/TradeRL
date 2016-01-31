using UnityEngine;

public class AttackAnimation : LocationTargetedAnimation {
	public CombatGraph combatGraph;
	public Character owner;
	public void Play(Vector2 location, System.Action finished, System.Action activated) {
		Character target = combatGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			AnimationController.Attack(owner.ownerGO, owner, target, finished, activated);
	}
}