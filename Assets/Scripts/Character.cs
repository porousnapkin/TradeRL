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
    public CombatController controller;
    public string displayName;
    public int speed = 10;
    public int positionIndex = 0;

    public System.Action<AIAbility> broadcastPreparedAIAbility = delegate { };
    public System.Action<AttackData> broadcastPreparedAttackEvent = delegate { };
    public System.Action actionFinishedEvent = delegate{};

    public void Setup(int hp) {
		health.MaxValue = hp;
		health.Value = health.MaxValue;
	}
}