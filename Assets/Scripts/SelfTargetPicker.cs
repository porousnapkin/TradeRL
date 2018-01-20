using UnityEngine;
using System.Collections.Generic;
using System;

public class SelfTargetPicker : AbilityTargetPicker {
	public Character owner;

	public void PickTargets(System.Action< List<Character> > pickedCallback) {
		var retVal = new List<Character>();
		retVal.Add(owner);

		pickedCallback(retVal);
	}

	public bool HasValidTarget() { return true; }

    public void CancelPicking()
    {
    }

    public void PrePickTargets(Action<List<Character>> targetsPicked)
    {
        var retVal = new List<Character>();
        retVal.Add(owner);

        targetsPicked(retVal);
    }
}
