public class AbilityFactory {
	public static MapGraph mapGraph;	
	public static DesertPathfinder pathfinding;
	public static TurnManager turnManager;

	public static PlayerAbility CreatePlayerAbility() {
		return new PlayerAbility(turnManager);
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