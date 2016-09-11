public class CombatModule {
	[Inject] public GlobalTextArea textArea {private get; set;}
	const int defaultRoll = 75;
	const int minRoll = 15;

	public void Attack(Character attacker, Character defender, bool isRangedAttack) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender, isRangedAttack);
		Hit(attack);
	}

    public void CustomAttack(Character attacker, Character target, int minDamage, int maxDamage, bool isRangedAttack, bool canCrit)
    {
        var attack = attacker.attackModule.CreateCustomAttack(attacker, target, minDamage, maxDamage, isRangedAttack, canCrit);
        Hit(attack);
    }

	public void Hit(AttackData data, string presentTenseVerb = "hits") {
		textArea.AddDamageLine(data, presentTenseVerb);
		data.target.health.Damage(data.totalDamage);
	}
}
