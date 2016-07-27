using System;
using System.Collections.Generic;

public interface AbilityModifier
{
	void BeforeActivation(CombatController owner, List<Character> targets);
	void ActivationEnded(CombatController owner, List<Character> targets);
}


