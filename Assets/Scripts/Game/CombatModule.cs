public class CombatModule {
	[Inject] public GlobalTextArea textArea {private get; set;}
	const int defaultRoll = 75;
	const int minRoll = 15;

	public void Attack(Character attacker, Character defender) {
		var attack = attacker.attackModule.CreateAttack(attacker, defender);
		Hit(attack);
	}	

	public void Hit(AttackData data, string presentTenseVerb = "hits") {
		textArea.AddDamageLine(data, presentTenseVerb);
		data.target.health.Damage(data.totalDamage);
	}
}
