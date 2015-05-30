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
		var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(ownerCharacter.WorldPosition, location);
		mapGraph.SetCharacterToPosition(ownerCharacter.WorldPosition, moveToPoint, ownerCharacter);

		Character target = mapGraph.GetPositionOccupant((int)location.x, (int)location.y);
		animation.Play(location, finishedAbility, () => Hit(ownerCharacter, target));
	}

	void Hit(Character attacker, Character defender) {
		var damage = defender.defenseModule.ModifyIncomingDamage((int)(attacker.attackModule.GetDamage() * damageMultiplier));
		GlobalTextArea.Instance.AddDamageLine(attacker, defender, presentTenseVerb, damage);
		defender.health.Damage(damage);
	}
}