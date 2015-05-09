public interface AbilityCost {
	bool CanAfford();
	void PayCost();
}

public class NoCost : AbilityCost {
	public bool CanAfford() { return true; }
	public void PayCost() {}
}