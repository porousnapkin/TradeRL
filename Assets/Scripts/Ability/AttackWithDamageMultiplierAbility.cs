using System.Collections.Generic;
using UnityEngine;

public class AttackWithDamageMultiplierAbility : AbilityActivator {
	public Character ownerCharacter;
	public MapGraph mapGraph;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "slams";

	public void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility) {
		Vector2 location = targets[0];
		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var damage = defender.defenseModule.ModifyIncomingDamage((int)(attacker.attackModule.GetDamage() * damageMultiplier));
		GlobalTextArea.Instance.AddDamageLine(attacker, defender, presentTenseVerb, damage);
		defender.health.Damage(damage);
	}
}