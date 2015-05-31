public class AbilityFactory {
	public static MapGraph mapGraph;	
	public static DesertPathfinder pathfinding;
	public static TurnManager turnManager;
	public static Effort effort;

	public static PlayerAbility CreatePlayerAbility() {
		var ability =  new PlayerAbility(turnManager);
		ability.effort = effort;
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