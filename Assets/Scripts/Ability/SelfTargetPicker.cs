using UnityEngine;
using System.Collections.Generic;

public class SelfTargetPicker : AbilityTargetPicker {
	public Character owner;

	public void PickTargets(System.Action< List<Vector2> > pickedCallback) {
		var retVal = new List<Vector2>();
		retVal.Add(owner.WorldPosition);

		pickedCallback(retVal);
	}

	public bool HasValidTarget() { return true; }
}
