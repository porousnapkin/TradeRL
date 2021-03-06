public class CombatModule {
	[Inject] public GlobalTextArea textArea {private get; set;}
	const int defaultRoll = 75;
	const int minRoll = 15;

	public void Attack(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		Hit(attack);
	}

	public void Hit(AttackData data, string presentTenseVerb = "hits", bool canCounter = true) {
		textArea.AddDamageLine(data, presentTenseVerb);
		data.target.health.Damage(data.totalDamage);

        if (canCounter && data.target.health.Value > 0)
            data.target.attackModule.CreateCounterAttacks(data).ForEach(a => Hit(a, "counter hits", false));
	}
}
