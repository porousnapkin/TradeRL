using System.Collections.Generic;
using UnityEngine;

public class AttackWithDamageMultiplierAbility : AbilityActivator {
	[Inject] public CombatGraph combatGraph {private get; set; }
	[Inject] public CombatModule combatModule {private get; set;}

	public Character ownerCharacter;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "slams";
	public string damageDescription = "Slam";

	public void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility) {
		Vector2 location = targets[0];
		Character target = combatGraph.GetPositionOccupant((int)location.x, (int)location.y);
		if(target != null)
			animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
        attack.damageModifiers.Add(new DamageModifierData
        {
            damageMod = Mathf.RoundToInt(attack.baseDamage * (1.0f - damageMultiplier)),
            damageModSource = damageDescription
        });
		combatModule.Hit(attack, presentTenseVerb);
	}
}