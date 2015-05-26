using System.Collections.Generic;
using UnityEngine;

public interface AbilityTargetPicker {
	void PickTargets(System.Action< List<Vector2> > pickedCallback);
	bool HasValidTarget();
}