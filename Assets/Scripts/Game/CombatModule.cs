using UnityEngine;

public class CombatModule {
	const float defaultRoll = 0.75f;
	const float minRoll = 0.15f;

	public float CalculateChanceToHit(Character attacker, Character defender) {
		int attackDefenseDiff = attacker.attackModule.GetAttackValue() - defender.defenseModule.GetDefenseValue();
		float rollValue = Mathf.Max(minRoll, defaultRoll + ((float)attackDefenseDiff / 100.0f));
		return rollValue;
	}

	public void Attack(Character attacker, Character defender) {
		if(Random.value < CalculateChanceToHit(attacker, defender))
			Hit(attacker, defender);
		else
			Miss();
	}	

	void Hit(Character attacker, Character defender) {
		defender.health.Damage(defender.defenseModule.ModifyIncomingDamage(attacker.attackModule.GetDamage()));
	}

	void Miss() {

	}
}
