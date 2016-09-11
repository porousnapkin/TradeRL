using System.Collections.Generic;

public interface PlayerActivatedPower
{
	int TurnsRemainingOnCooldown { get; }
	bool CanUse();
	string GetName();
    void PayCosts();
    void RefundCosts();
    void Activate(System.Action callback);

    List<Cost> GetCosts();
    List<Restriction> GetRestrictions();
}