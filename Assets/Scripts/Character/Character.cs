using UnityEngine;

public class Character {
	public const string PLAYER = "PlayerCharacter";

	public Health health = new Health();
	public Vector2 Position { get; set; }
    public bool IsInMelee { get; set; }
	public AttackModule attackModule;
	public DefenseModule defenseModule;
	public Faction myFaction;
	public GameObject ownerGO;
	public string displayName;

	public void Setup(int hp) {
		health.MaxValue = hp;
		health.Value = health.MaxValue;
	}
}