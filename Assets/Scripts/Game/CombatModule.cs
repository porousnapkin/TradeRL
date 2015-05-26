using UnityEngine;

public class CombatModule {
	public event System.Action<Character> MissedEvent = delegate{};
	const int defaultRoll = 75;
	const int minRoll = 15;

	public static float CalculateChanceToHit(Character attacker, Character defender) {
		int attackDefenseDiff = attacker.attackModule.GetAttackValue() - defender.defenseModule.GetDefenseValue();
		int rollValue = Mathf.Max(minRoll, defaultRoll + attackDefenseDiff);
		return rollValue;
	}

	public void Attack(Character attacker, Character defender) {
		if(Random.Range(0, 100) < CalculateChanceToHit(attacker, defender))
			Hit(attacker, defender);
		else
			Miss(attacker, defender);
	}	

	void Hit(Character attacker, Character defender) {
		var damage = defender.defenseModule.ModifyIncomingDamage(attacker.attackModule.GetDamage());
		GlobalTextArea.Instance.AddDamageLineWithChanceToHit(attacker, defender, "hits", damage);
		defender.health.Damage(damage);
	}

	void Miss(Character attacker, Character defender) {
		GlobalTextArea.Instance.AddMissLineWithChanceToHit(attacker, defender, "misses");
		MissedEvent(defender);
	}
}
