using System;
using System.Collections.Generic;

public interface AbilityActivator {
	void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility);
    void PrepareActivation(List<Character> targets, TargetedAnimation animation, Action preparedCallback);
}

