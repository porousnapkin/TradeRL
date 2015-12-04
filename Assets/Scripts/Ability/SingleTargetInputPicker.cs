using UnityEngine;
using System.Collections.Generic;

public class SingleTargetInputPicker : AbilityTargetPicker {
	System.Action< List<Vector2> > pickedCallback;
	public GridInputCollector inputCollector;
	List<InputTargetFilter> targetFilters = new List<InputTargetFilter>();
	public GridHighlighter gridHighlighter;
	public int minRange = 1;
	public int maxRange = 1;
	public Character owner;

	public void AddFilter(InputTargetFilter targetFilter) {
		targetFilters.Add(targetFilter);
	}

	public void PickTargets(System.Action< List<Vector2> > pickedCallback) {
		this.pickedCallback = pickedCallback;

		gridHighlighter.DrawRangeFromPoint(owner.GraphPosition, minRange, maxRange);
		inputCollector.OverrideInput(LocationHit);
	}

	void LocationHit(Vector2 location) {
		gridHighlighter.HideHighlights();

		if(DoesLocationPassFilters(location) && InRange(location))
			AppropriateLocationHit(location);
		else
			InappropriateLocationHit();
	}

	bool InRange(Vector2 location) {
		Vector2 diff = location - owner.GraphPosition;
		return diff.x <= maxRange && diff.x >= -maxRange && diff.y <= maxRange && diff.y >= -maxRange && 
			!(diff.x < minRange && diff.x > -minRange && diff.y < minRange && diff.y > -minRange);
	}

	bool DoesLocationPassFilters(Vector2 location) {
		foreach(var filter in targetFilters) 
			if(!filter.PassesFilter(owner, location)) 
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
		for(int x = -maxRange; x <= maxRange; x++) {
			for(int y = -maxRange; y <= maxRange; y++) {
				if(x < minRange && x > -minRange && y < minRange && y > -minRange)
					continue;

				Vector2 checkPoint = owner.GraphPosition + new Vector2(x, y);
				if(Grid.IsValidPosition((int)checkPoint.x, (int)checkPoint.y) && DoesLocationPassFilters(checkPoint)) 
					return true;
			}
		}
		
		return false; 
	}
}