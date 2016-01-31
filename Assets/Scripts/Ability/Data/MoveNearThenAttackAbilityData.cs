public class MoveNearThenAttackAbilityData : AbilityActivatorData {
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "charges";

	public override AbilityActivator Create(Character owner) {
		var a = DesertContext.StrangeNew<MoveNearThenAttackAbility>();

		a.ownerCharacter = owner;
		a.damageMultiplier = damageMultiplier;
		a.presentTenseVerb = presentTenseVerb;
		
		return a;
	}
}