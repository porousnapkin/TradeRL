using UnityEngine;

public class AIAttackModule : AttackModule {
	public int attackValue = 10;
	public int minDamage = 10;
	public int maxDamage = 12;

	public int GetAttackValue() { return attackValue; }
	public int GetDamage() { return Random.Range(minDamage, maxDamage); }
}