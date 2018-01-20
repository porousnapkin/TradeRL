using System.Collections.Generic;

public interface AbilityModifier
{
    void PrepareActivation(List<Character> targets, System.Action callback);
	void BeforeActivation(List<Character> targets, System.Action callback);
	void ActivationEnded(List<Character> targets, System.Action callback);
}


