using UnityEngine;

public class Character {
	public Health health;
	public Vector2 WorldPosition { get; set; }
	public AttackModule attackModule;
	public DefenseModule defenseModule;
	public Faction myFaction;
	public GameObject ownerGO;

	public Character() {
		health = new Health();
		health.MaxValue = 50;
		health.Value = health.MaxValue;

		attackModule = new TestAttackModule();
		defenseModule = new TestDefenseModule();
	}
}