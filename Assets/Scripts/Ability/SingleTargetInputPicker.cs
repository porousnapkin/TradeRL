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

		if(DoesLocationPassFilters(location))
			AppropriateLocationHit(location);
		else
			InappropriateLocationHit();
	}

	bool DoesLocationPassFilters(Vector2 location) {
		foreach(var filter in targetFilters) 
			if(!filter.PassesFilter(location)) 
				return false;

		return true;
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

	public bool HasValidTarget() { 
		for(int x = -range; x <= range; x++) {
			for(int y = -range; y <= range; y++) {
				if(x == 0 && y == 0)
					continue;

				Vector2 checkPoint = owner.WorldPosition + new Vector2(x, y);
				if(Grid.IsValidPosition((int)checkPoint.x, (int)checkPoint.y) && DoesLocationPassFilters(checkPoint)) 
					return true;
			}
		}
		
		return false; 
	}
}