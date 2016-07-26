using System;
using System.Collections.Generic;

public interface AbilityModifier
{
	void BeforeActivation(Character owner, List<Character> targets);
	void ActivationEnded(Character owner, List<Character> targets);
}


