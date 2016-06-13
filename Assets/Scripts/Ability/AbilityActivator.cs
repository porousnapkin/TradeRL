using System.Collections.Generic;
using UnityEngine;

public interface AbilityActivator {
	void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility);
}