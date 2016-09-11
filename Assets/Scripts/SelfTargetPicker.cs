using UnityEngine;
using System.Collections.Generic;

public class SelfTargetPicker : AbilityTargetPicker {
	public Character owner;

	public void PickTargets(System.Action< List<Character> > pickedCallback) {
		var retVal = new List<Character>();
		retVal.Add(owner);

		pickedCallback(retVal);
	}

	public bool HasValidTarget() { return true; }
}
