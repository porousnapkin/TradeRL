using System.Collections.Generic;
using UnityEngine;

public class AttackWithDamageMultiplierAbility : AbilityActivator {
	public Character ownerCharacter;
	public MapGraph mapGraph;
	public float damageMultiplier = 2.0f;
	public string attackName = "Slam";

	public void Activate(List<Vector2> targets, System.Action finishedAbility) {
		Vector2 location = targets[0];
		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			AnimationController.Attack(ownerCharacter.ownerGO, ownerCharacter, target, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var damage = defender.defenseModule.ModifyIncomingDamage((int)(attacker.attackModule.GetDamage() * damageMultiplier));
		GlobalTextArea.Instance.AddLine(attacker.displayName + " hits " + defender.displayName + 
			" for <color=Red>" + damage + "</color> damage. (" + attackName + ")");
		defender.health.Damage(damage);
	}
}