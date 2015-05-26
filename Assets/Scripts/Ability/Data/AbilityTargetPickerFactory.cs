using System.Collections.Generic;

public class AbilityTargetPickerFactory {
	public static MapGraph mapGraph;

	public static AbilityTargetPicker CreateAIWeakestTargetPicker(Character owner, int range, List<InputTargetFilterData> targetFilters) {
		var picker = new AIWeakestTargetPicker();
		picker.range = range;
		picker.mapGraph = mapGraph;
		picker.owner = owner;

		foreach(var filter in targetFilters)
			picker.AddFilter(filter.Create(owner));

		return picker;
	}
}