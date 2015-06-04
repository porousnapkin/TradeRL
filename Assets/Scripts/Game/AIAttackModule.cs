using UnityEngine;
using System.Collections.Generic;

public class AIAttackModule : AttackModule {
	public int attackValue = 10;
	public int minDamage = 10;
	public int maxDamage = 12;
	public MapGraph mapGraph;

	public AttackData CreateAttack(Character attacker, Character target) {
		var data = new AttackData();	
		data.attackRoll = CombatModule.GetAttackRoll();
		data.didHit = CombatModule.DidHit(data.attackRoll, attacker, target);
		data.damage = GetDamage(data.attackRoll, attacker, target);
		data.notes = GetNotes(data, attacker, target);
		return data;
	}

	public int GetAttackValue() { return attackValue; }

	int GetDamage(int attackRoll, Character attacker, Character target) { 
		float bonusMult = 1.0f;

		if(isFlanking(target))
			bonusMult += GlobalVariables.flankingDamageBonus;
		if(isCrit(attackRoll, attacker, target))
			bonusMult += GlobalVariables.critDamageBonus;

		return Mathf.RoundToInt(Random.Range(minDamage, maxDamage) * bonusMult); 
	}

	bool isFlanking(Character target) {
		return mapGraph.GetNumAdjacentEnemies(target) > 1;
	}

	bool isCrit(int attackRoll, Character attacker, Character defender) {
		return (attackRoll - CombatModule.AttackRollOffset(attacker, defender)) >= GlobalVariables.attackRollToCrit;
	}

	List<string> GetNotes(AttackData data, Character attacker, Character target) { 
		var strings = new List<string>(); 
		if(isFlanking(target))
			strings.Add("Flanked");
		if(isCrit(data.attackRoll, attacker, target))
			strings.Add("Critical");

		return strings;
	}
}