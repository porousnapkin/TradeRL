using System.Collections.Generic;
using UnityEngine;

public class MoveNearThenAttackAbility : AbilityActivator {
	[Inject] public CombatGraph combatGraph { private get; set; }
	[Inject] public CombatModule combatModule { private get; set; }
	[Inject(DesertPathfinder.COMBAT)] public DesertPathfinder pathfinding {private get; set;}

	public Character ownerCharacter;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "charges";
	Vector2 location;
	System.Action finishedAbility;

	public void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility) {
		location = targets[0];
#warning "Commenting this out, need to reimplement in pathfinder :("
		//var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(ownerCharacter.Position, location, combatGraph);
		var moveToPoint = Vector2.zero;
		combatGraph.SetCharacterToPosition(ownerCharacter.Position, moveToPoint, ownerCharacter);

		Character target = combatGraph.GetPositionOccupant((int)location.x, (int)location.y);
		animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		attack.damage = Mathf.RoundToInt(attack.damage * damageMultiplier);
		combatModule.Hit(attack, presentTenseVerb);
	}
}