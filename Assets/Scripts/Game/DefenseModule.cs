public interface DefenseModule {
	int GetDefenseValue();
	int ModifyIncomingDamage(int damage);
}

public class TestDefenseModule : DefenseModule {
	public int GetDefenseValue() { return 10; }
	public int ModifyIncomingDamage(int damage) { return damage; }
}