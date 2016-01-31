using UnityEngine;

public class Character {
	public const string PLAYER = "PlayerCharacter";

	public Health health;
	public Vector2 Position { get; set; }
	public AttackModule attackModule;
	public DefenseModule defenseModule;
	public Faction myFaction;
	public GameObject ownerGO;
	public string displayName;

	public void Setup(int hp) {
		health = new Health();
		health.MaxValue = hp;
		health.Value = health.MaxValue;
	}
}