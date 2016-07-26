public interface PlayerActivatedPower
{
	int TurnsRemainingOnCooldown { get; }
	bool CanUse();
	string GetName();
}