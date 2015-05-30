using System.Collections.Generic;
using UnityEngine;

public interface AbilityActivator {
	void Activate(List<Vector2> targets, LocationTargetedAnimation animation, System.Action finishedAbility);
}