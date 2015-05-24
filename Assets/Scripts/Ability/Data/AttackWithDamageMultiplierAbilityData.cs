public class AttackWithDamageMultiplierAbilityData : AbilityActivatorData {
	public Character ownerCharacter;

	public MapGraph mapGraph;
	public float damageMultiplier = 2.0f;
	public string attackName = "Slam";

	public override AbilityActivator Create(Character owner) {
		var a = new AttackWithDamageMultiplierAbility();
		a.mapGraph = MapGraph.Instance;
		a.ownerCharacter = owner;
		a.damageMultiplier = damageMultiplier;
		a.attackName = attackName;

		return a;
	}
}