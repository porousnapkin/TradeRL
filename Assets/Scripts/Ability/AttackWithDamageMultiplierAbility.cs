using System.Collections.Generic;
using UnityEngine;

public class AttackWithDamageMultiplierAbility : AbilityActivator {
	[Inject] public CombatGraph combatGraph {private get; set; }

	public Character ownerCharacter;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "slams";

	public void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility) {
		Vector2 location = targets[0];
		Character target = combatGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		attack.damage = Mathf.RoundToInt(attack.damage * damageMultiplier);
		CombatModule.Hit(attack, presentTenseVerb);
	}
}