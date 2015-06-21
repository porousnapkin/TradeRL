using UnityEngine;
using System.Collections.Generic;

public class AIAttackModule : AttackModule {
	public int attackValue = 10;
	public int minDamage = 10;
	public int maxDamage = 12;
	public MapGraph mapGraph;

	public AttackData CreateAttack(Character attacker, Character target) {
		var data = new AttackData();	
		data.attacker = attacker;
		data.target = target;
		data.attackRoll = CombatModule.GetAttackRoll();
		data.didHit = CombatModule.DidHit(data.attackRoll, attacker, target);
		data.damage = Random.Range(minDamage, maxDamage);
		data.notes = CombatModule.GetNotes(data, mapGraph);

		data.damage = CombatModule.GetModifiedDamage(data, mapGraph);
		return data;
	}

	public int GetAttackValue() { return attackValue; }
}