public class GainHealthAfterCombat {
    [Inject] public GlobalTextArea textArea { private get; set; }
	Health health;
	public int amount;
    public string source { private get; set; }

	public void Apply(Health health, int amount) 
	{
		this.health = health;

		GlobalEvents.CombatEnded += CombatEnded;
	}

	void CombatEnded() 
	{
        if(health.Value < health.MaxValue)
        {
		    health.Heal(amount);
            textArea.AddLine(source + " recovered " + amount + " hp after battle");
        }
	}

	public void Remove() 
	{
		GlobalEvents.CombatEnded -= CombatEnded;
	}
}
