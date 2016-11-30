public class GainHealthAfterCombat {
	Health health;
	int amount;

	public void Apply(Health health, int amount) 
	{
		this.health = health;

		GlobalEvents.CombatEnded += CombatEnded;
	}

	void CombatEnded() 
	{
		health.Heal(amount);
	}

	public void Remove() 
	{
		GlobalEvents.CombatEnded -= CombatEnded;
	}
}
