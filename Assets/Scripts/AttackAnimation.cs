using UnityEngine;

public class AttackAnimation : TargetedAnimation {
	public Character owner;

	public void Play(Character target, System.Action finished, System.Action activated) {
		AnimationController.Attack(owner.ownerGO, owner, target, finished, activated);
	}
}