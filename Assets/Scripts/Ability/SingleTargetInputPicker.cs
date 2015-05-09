using UnityEngine;
using System.Collections.Generic;

public class SingleTargetInputPicker : AbilityTargetPicker {
	System.Action< List<Vector2> > pickedCallback;
	public GridInputCollector inputCollector;
	List<InputTargetFilter> targetFilters = new List<InputTargetFilter>();
	public GridHighlighter gridHighlighter;
	public int range = 1;
	public Character owner;

	public void AddFilter(InputTargetFilter targetFilter) {
		targetFilters.Add(targetFilter);
	}

	public void PickTargets(System.Action< List<Vector2> > pickedCallback) {
		this.pickedCallback = pickedCallback;

		gridHighlighter.DrawRangeFromPoint(owner.WorldPosition, range);
		inputCollector.OverrideInput(LocationHit);
	}

	void LocationHit(Vector2 location) {
		gridHighlighter.HideHighlights();

		foreach(var filter in targetFilters) {
			if(!filter.PassesFilter(location)) {
				InappropriateLocationHit();
				return;
			}
		}

		AppropriateLocationHit(location);	
	}

	void InappropriateLocationHit() {
		inputCollector.FinishOverridingInput();
	}

	void AppropriateLocationHit(Vector2 location) {
		var targets = new List<Vector2>();
		targets.Add(location);
		inputCollector.FinishOverridingInput();

		pickedCallback(targets);
	} 
}