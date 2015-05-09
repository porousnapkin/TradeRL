using System.Collections.Generic;
using UnityEngine;

public interface AbilityActivator {
	void Activate(List<Vector2> targets, System.Action finishedAbility);
}