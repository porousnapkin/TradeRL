using UnityEngine;

public interface AttackModule {
	int GetAttackValue();
	int GetDamage();
}

public class TestAttackModule : AttackModule {
	public int GetAttackValue() { return 10; }
	public int GetDamage() { return Random.Range(4, 6); }
}