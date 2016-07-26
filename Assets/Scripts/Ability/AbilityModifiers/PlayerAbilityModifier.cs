using System.Collections.Generic;

public class PlayerAbilityModifier : PlayerActivatedPower
{
	public AbilityModifier abilityModifier;
	public string name;
	public int cooldown = 4;
	int turnsOnCooldown = 0;

	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; } }

	public bool CanUse()
	{
		return TurnsRemainingOnCooldown <= 0;
	}

	public string GetName()
	{
		return name;
	}

	public AbilityModifier GetAbilityModifier()
	{
		return abilityModifier;
	}

	public void TurnEnd()
	{
		turnsOnCooldown--;
	}

	public void Activate(Character owner, List<Character> targets)
	{
		turnsOnCooldown = cooldown;
		abilityModifier.BeforeActivation(owner, targets);
	}

	public void Finish(Character owner, List<Character> targets)
	{
		abilityModifier.ActivationEnded(owner, targets);
	}
}

