using System.Collections.Generic;

public interface DefenseModule {
	int GetDefenseValue();
	int ModifyIncomingDamage(int damage);
	List<string> GetNotes(Character attacker);
}

public class TestDefenseModule : DefenseModule {
	public int GetDefenseValue() { return 10; }
	public int ModifyIncomingDamage(int damage) { return damage; }
	public List<string> GetNotes(Character attacker) { return new List<string>(); }
}