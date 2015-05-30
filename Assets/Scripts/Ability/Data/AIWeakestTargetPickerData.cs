using System.Collections.Generic;

public class AIWeakestTargetPickerData : AbilityTargetPickerData {
	public int minRange = 1;
	public int maxRange = 1;
	public List<InputTargetFilterData> targetFilters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner) {
		return AbilityTargetPickerFactory.CreateAIWeakestTargetPicker(owner, minRange, maxRange, targetFilters);
	}
}