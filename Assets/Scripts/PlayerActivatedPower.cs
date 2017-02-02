using System.Collections.Generic;

public interface PlayerActivatedPower
{
	int TurnsRemainingOnCooldown { get; }
	bool CanUse();
	string GetName();
	string GetDescription();
    void PayCosts();
    void RefundCosts();
    void Activate(System.Action callback);

    List<Visualizer> GetVisualizers();
}