using UnityEngine;
using System.Collections.Generic;

public class CombatModule {
	[Inject] public GlobalTextArea textArea {private get; set;}
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

	public void Attack(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		if(attack.didHit)
			Hit(attack);
		else
			Miss(attacker, defender);
	}	

	public void Hit(AttackData data, string presentTenseVerb = "hits") {
		var damage = data.target.defenseModule.ModifyIncomingDamage(Mathf.RoundToInt(data.damage));

		textArea.AddDamageLineWithChanceToHit(data.attacker, data.target, presentTenseVerb, 
			damage, GlobalTextArea.CreateNotes(data.notes));
		data.target.health.Damage(damage);
	}

	public void Miss(Character attacker, Character defender) {
		textArea.AddMissLineWithChanceToHit(attacker, defender, "misses");
	}

	public static int GetModifiedDamage(AttackData data, CombatGraph combatGraph) { 
		float bonusMult = 1.0f;

		if(isFlanking(data.target, combatGraph))
			bonusMult += GlobalVariables.flankingDamageBonus;
		if(isCrit(data.attackRoll, data.attacker, data.target))
			bonusMult += GlobalVariables.critDamageBonus;

		return Mathf.RoundToInt(data.damage * bonusMult); 
	}

	static bool isFlanking(Character target, CombatGraph combatGraph) {
		return combatGraph.GetNumAdjacentEnemies(target) > 1;
	}

	static bool isCrit(int attackRoll, Character attacker, Character defender) {
		return (attackRoll + AttackRollOffset(attacker, defender)) >= GlobalVariables.attackRollToCrit;
	}

	public static List<string> GetNotes(AttackData data, CombatGraph combatGraph) { 
		var strings = new List<string>(); 
		if(isFlanking(data.target, combatGraph))
			strings.Add("Flanked");
		if(isCrit(data.attackRoll, data.attacker, data.target))
			strings.Add("Critical");

		return strings;
	}
}
