using System.Collections.Generic;
using UnityEngine;

public interface AbilityTargetPicker {
	void PickTargets(System.Action< List<Character> > pickedCallback);
	bool HasValidTarget();
}