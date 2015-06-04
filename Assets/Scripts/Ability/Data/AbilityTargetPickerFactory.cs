using System.Collections.Generic;

public class AbilityTargetPickerFactory {
	public static MapGraph mapGraph;
	public static DesertPathfinder pathfinding;

	public static AbilityTargetPicker CreateAIWeakestTargetPicker(Character owner, int minRange, int maxRange, List<InputTargetFilterData> targetFilters) {
		var picker = new AIWeakestTargetPicker();
		picker.minRange = minRange;
		picker.maxRange = maxRange;
		picker.mapGraph = mapGraph;
		picker.owner = owner;

		foreach(var filter in targetFilters)
			picker.AddFilter(filter.Create(owner));

		return picker;
	}

	public static TargetHasAdjacentSpacetoMoveInto CreateTargetHasAdjacentSpacetoMoveInto() {
		var filter = new TargetHasAdjacentSpacetoMoveInto();
		filter.pathfinding = pathfinding;
		filter.mapGraph = mapGraph;
		return filter;
	}
}