public class MoveNearThenAttackAbilityData : AbilityActivatorData {
	public MapGraph mapGraph;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "charges";

	public override AbilityActivator Create(Character owner) {
		var a = AbilityFactory.CreateMoveNearThenAttack();
		a.ownerCharacter = owner;
		a.damageMultiplier = damageMultiplier;
		a.presentTenseVerb = presentTenseVerb;

		return a;
	}
}