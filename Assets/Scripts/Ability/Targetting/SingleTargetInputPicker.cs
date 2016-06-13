using UnityEngine;
using System.Collections.Generic;

//TODO: FIX THIS!!!
public class SingleTargetInputPicker : AbilityTargetPicker {
	[Inject] public GridInputCollector gridInputCollector { private get; set; }

	System.Action< List<Character> > pickedCallback;
	List<InputTargetFilter> targetFilters = new List<InputTargetFilter>();

	public GridHighlighter gridHighlighter;
	public int minRange { private get; set; }
	public int maxRange { private get; set; }
	public Character owner { private get; set; }

	public void AddFilter(InputTargetFilter targetFilter) {
		targetFilters.Add(targetFilter);
	}

	public void PickTargets(System.Action< List<Character> > pickedCallback) {
		this.pickedCallback = pickedCallback;

		gridHighlighter.DrawRangeFromPoint(owner.Position, minRange, maxRange);
		gridInputCollector.OverrideInput(LocationHit);
	}

	void LocationHit(Vector2 location) {
		gridHighlighter.HideHighlights();

		if(DoesLocationPassFilters() && InRange(location))
			AppropriateLocationHit(location);
		else
			InappropriateLocationHit();
	}

	bool InRange(Vector2 location) {
		Vector2 diff = location - owner.Position;
		return diff.x <= maxRange && diff.x >= -maxRange && diff.y <= maxRange && diff.y >= -maxRange && 
			!(diff.x < minRange && diff.x > -minRange && diff.y < minRange && diff.y > -minRange);
	}

	bool DoesLocationPassFilters() {
        //TODO
		//foreach(var filter in targetFilters) 
		//	if(!filter.PassesFilter(owner)) 
		//		return false;

		return true;
	}

	void InappropriateLocationHit() {
		gridInputCollector.FinishOverridingInput();
	}

	void AppropriateLocationHit(Vector2 location) {
		var targets = new List<Vector2>();
		targets.Add(location);
		gridInputCollector.FinishOverridingInput();

//		pickedCallback(targets);
	} 

	public bool HasValidTarget() { 
		for(int x = -maxRange; x <= maxRange; x++) {
			for(int y = -maxRange; y <= maxRange; y++) {
				if(x < minRange && x > -minRange && y < minRange && y > -minRange)
					continue;

				Vector2 checkPoint = owner.Position + new Vector2(x, y);
				if(Grid.IsValidPosition((int)checkPoint.x, (int)checkPoint.y) && DoesLocationPassFilters()) 
					return true;
			}
		}
		
		return false; 
	}
}