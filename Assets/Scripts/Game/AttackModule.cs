using UnityEngine;
using System.Collections.Generic;

public class AttackData {
	public int attackRoll;
	public bool didHit;
	public int damage;
	public List<string> notes;

	public Character attacker;
	public Character target;
}

public interface AttackModule {
	AttackData CreateAttack(Character attacker, Character target);		
	int GetAttackValue();
}

public class TestAttackModule : AttackModule {
	public CombatGraph combatGraph;

	public AttackData CreateAttack(Character attacker, Character target) {
		var data = new AttackData();	
		data.attacker = attacker;
		data.target = target;
		data.attackRoll = CombatModule.GetAttackRoll();
		data.didHit = CombatModule.DidHit(data.attackRoll, attacker, target);
		data.damage = Random.Range(10, 12);
		data.notes = CombatModule.GetNotes(data, combatGraph);

		data.damage = CombatModule.GetModifiedDamage(data, combatGraph);
		return data;
	}

	public int GetAttackValue() { return 10; }
}