public class AbilityFactory {
	public static MapGraph mapGraph;	
	public static DesertPathfinder pathfinding;
	public static TurnManager turnManager;
	public static Effort effort;
	public static DooberFactory dooberFactory;

	public static PlayerAbility CreatePlayerAbility(Character owner) {
		var ability =  new PlayerAbility(turnManager);
		ability.effort = effort;
		ability.dooberFactory = dooberFactory;
		ability.character = owner;
		return ability;
	}

	public static AttackWithDamageMultiplierAbility CreateAttackWithDamageMultiplierAbility() {
		var ability = new AttackWithDamageMultiplierAbility();
		ability.mapGraph = mapGraph;
		return ability;
	}	

	public static MoveNearThenAttackAbility CreateMoveNearThenAttack() {
		var a = new MoveNearThenAttackAbility();
		a.mapGraph = mapGraph;
		a.pathfinding = pathfinding;
		return a;
	}
}