using UnityEngine;
using System.Collections.Generic;

public class AIWeakestTargetPicker : AbilityTargetPicker {
	List<InputTargetFilter> targetFilters = new List<InputTargetFilter>();
	public int range = 1;
	public MapGraph mapGraph;
	public Character owner;

	public void AddFilter(InputTargetFilter targetFilter) {
		targetFilters.Add(targetFilter);
	}

	public void PickTargets(System.Action< List<Vector2> > pickedCallback) {
		var retVal = new List<Vector2>();
		var possibleTargets = GetValidTargets();

		possibleTargets.Sort((first, second) => first.health.Value - second.health.Value);

		retVal.Add(possibleTargets[0].WorldPosition);

		pickedCallback(retVal);
	}

	List<Character> GetValidTargets() {
		var retVal = new List<Character>();
		for(int x = -range; x <= range; x++) {
			for(int y = -range; y <= range; y++) {
				if(x == 0 && y == 0)
					continue;

				Vector2 checkPoint = owner.WorldPosition + new Vector2(x, y);
				if(!Grid.IsValidPosition((int)checkPoint.x, (int)checkPoint.y))
					continue;

				Character occupant = mapGraph.GetPositionOccupant((int)checkPoint.x, (int)checkPoint.y);
				if(occupant != null && DoesLocationPassFilters(checkPoint)) 
					retVal.Add(occupant);
			}
		}

		return retVal;
	}

	bool DoesLocationPassFilters(Vector2 location) {
		foreach(var filter in targetFilters) 
			if(!filter.PassesFilter(location)) 
				return false;

		return true;
	}

	public bool HasValidTarget() { 
		return GetValidTargets().Count > 0;
	}
}