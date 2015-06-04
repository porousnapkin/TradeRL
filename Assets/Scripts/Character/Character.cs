using UnityEngine;

public class Character {
	public Health health;
	public Vector2 WorldPosition { get; set; }
	public AttackModule attackModule;
	public DefenseModule defenseModule;
	public Faction myFaction;
	public GameObject ownerGO;
	public string displayName;

	public Character(int hp) {
		health = new Health();
		health.MaxValue = hp;
		health.Value = health.MaxValue;
	}
}