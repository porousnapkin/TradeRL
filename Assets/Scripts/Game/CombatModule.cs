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
			Hit(attack, attacker, defender);
		else
			Miss(attacker, defender);
	}	

	public static void Hit(AttackData attack, Character attacker, Character defender, string presentTenseVerb = "hits") {
		var damage = defender.defenseModule.ModifyIncomingDamage(Mathf.RoundToInt(attack.damage));

		GlobalTextArea.Instance.AddDamageLineWithChanceToHit(attacker, defender, presentTenseVerb, damage, GlobalTextArea.Instance.CreateNotes(attack.notes));
		defender.health.Damage(damage);
	}

	public static void Miss(Character attacker, Character defender) {
		GlobalTextArea.Instance.AddMissLineWithChanceToHit(attacker, defender, "misses");
	}
}
