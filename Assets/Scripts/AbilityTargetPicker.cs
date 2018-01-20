using System;
using System.Collections.Generic;
using UnityEngine;

public interface AbilityTargetPicker {
	void PickTargets(System.Action< List<Character> > pickedCallback);
	bool HasValidTarget();
    void CancelPicking();
    void PrePickTargets(Action<List<Character>> targetsPicked);
}