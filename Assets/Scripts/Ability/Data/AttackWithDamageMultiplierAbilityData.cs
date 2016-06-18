public class AttackWithDamageMultiplierAbilityData : AbilityActivatorData {
	public float damageMultiplier = 2.0f;
	public string presentTenseVerb = "slams";
    public bool isRangedAttack = false;

	public override AbilityActivator Create(CombatController owner) {
		var a = DesertContext.StrangeNew<AttackWithDamageMultiplierAbility>();

		a.ownerCharacter = owner.GetCharacter();
		a.damageMultiplier = damageMultiplier;
		a.presentTenseVerb = presentTenseVerb;
        a.isRangedAttack = isRangedAttack;

		return a;
	}
}