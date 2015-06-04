using UnityEngine;

public class AIDefenseModule : DefenseModule {
	public int defenseValue = 10;
	public int damageReduction = 0;

	public int GetDefenseValue() { return defenseValue; }
	public int ModifyIncomingDamage(int damage) { return Mathf.Max(0, damage - damageReduction); }
}