public class AttackWithDamageMultiplierAbilityData : AbilityActivatorData {
	public MapGraph mapGraph;
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "slams";

	public override AbilityActivator Create(Character owner) {
		var a = AbilityFactory.CreateAttackWithDamageMultiplierAbility();
		a.ownerCharacter = owner;
		a.damageMultiplier = damageMultiplier;
		a.presentTenseVerb = presentTenseVerb;

		return a;
	}
}