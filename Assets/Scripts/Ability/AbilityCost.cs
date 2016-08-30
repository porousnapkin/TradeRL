using System;

public interface AbilityCost {
	bool CanAfford();
	void PayCost();
    void Refund();
}