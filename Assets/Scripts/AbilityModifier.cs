using System;
using System.Collections.Generic;

public interface AbilityModifier
{
	void BeforeActivation(List<Character> targets, System.Action callback);
	void ActivationEnded(List<Character> targets, System.Action callback);
}


