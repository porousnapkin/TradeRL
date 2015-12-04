using System.Collections.Generic;
using UnityEngine;

public class MoveNearThenAttackAbility : AbilityActivator {
	public Character ownerCharacter;
	public MapGraph mapGraph;
	public DesertPathfinder pathfinding;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "charges";
	Vector2 location;
	System.Action finishedAbility;

	public void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility) {
		location = targets[0];
		var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(ownerCharacter.GraphPosition, location, mapGraph);
		mapGraph.SetCharacterToPosition(ownerCharacter.GraphPosition, moveToPoint, ownerCharacter);

		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		attack.damage = Mathf.RoundToInt(attack.damage * damageMultiplier);
		CombatModule.Hit(attack, presentTenseVerb);
	}
}