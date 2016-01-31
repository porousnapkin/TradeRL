using System.Collections.Generic;

public class AIWeakestTargetPickerData : AbilityTargetPickerData {
	public int minRange = 1;
	public int maxRange = 1;
	public List<InputTargetFilterData> targetFilters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner) {
		var picker = DesertContext.StrangeNew<AIWeakestTargetPicker>();
		picker.minRange = minRange;
		picker.maxRange = maxRange;
		picker.owner = owner;
		
		foreach(var filter in targetFilters)
			picker.AddFilter(filter.Create(owner));
		
		return picker;
	}
}