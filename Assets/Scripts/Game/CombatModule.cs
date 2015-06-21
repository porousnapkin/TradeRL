using UnityEngine;
using System.Collections.Generic;

public class CombatModule {
	const int defaultRoll = 75;
	const int minRoll = 15;

	public static int GetAttackRoll() {
		return Random.Range(0, 100);
	}

	public static int AttackRollOffset(Character attacker, Character defender) {
		return attacker.attackModule.GetAttackValue() - defender.defenseModule.GetDefenseValue();
	}

	public static float CalculateChanceToHit(Character attacker, Character defender) {
		int attackDefenseDiff = attacker.attackModule.GetAttackValue() - defender.defenseModule.GetDefenseValue();
		int rollValue = Mathf.Max(minRoll, defaultRoll + attackDefenseDiff);
		return rollValue;
	}

	public static bool DidHit(int attackRoll, Character attacker, Character defender) {
		return attackRoll < CalculateChanceToHit(attacker, defender);
	}

	public static void Attack(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		if(attack.didHit)
			Hit(attack);
		else
			Miss(attacker, defender);
	}	

	public static void Hit(AttackData data, string presentTenseVerb = "hits") {
		var damage = data.target.defenseModule.ModifyIncomingDamage(Mathf.RoundToInt(data.damage));

		GlobalTextArea.Instance.AddDamageLineWithChanceToHit(data.attacker, data.target, presentTenseVerb, 
			damage, GlobalTextArea.Instance.CreateNotes(data.notes));
		data.target.health.Damage(damage);
	}

	public static void Miss(Character attacker, Character defender) {
		GlobalTextArea.Instance.AddMissLineWithChanceToHit(attacker, defender, "misses");
	}

	public static int GetModifiedDamage(AttackData data, MapGraph mapGraph) { 
		float bonusMult = 1.0f;

		if(isFlanking(data.target, mapGraph))
			bonusMult += GlobalVariables.flankingDamageBonus;
		if(isCrit(data.attackRoll, data.attacker, data.target))
			bonusMult += GlobalVariables.critDamageBonus;

		return Mathf.RoundToInt(data.damage * bonusMult); 
	}

	static bool isFlanking(Character target, MapGraph mapGraph) {
		return mapGraph.GetNumAdjacentEnemies(target) > 1;
	}

	static bool isCrit(int attackRoll, Character attacker, Character defender) {
		return (attackRoll + AttackRollOffset(attacker, defender)) >= GlobalVariables.attackRollToCrit;
	}

	public static List<string> GetNotes(AttackData data, MapGraph mapGraph) { 
		var strings = new List<string>(); 
		if(isFlanking(data.target, mapGraph))
			strings.Add("Flanked");
		if(isCrit(data.attackRoll, data.attacker, data.target))
			strings.Add("Critical");

		return strings;
	}
}
