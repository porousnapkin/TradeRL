using System.Collections.Generic;

public class SingleTargetInputPickerData : AbilityTargetPickerData {
	public int minRange = 1;
	public int maxRange = 1;
	public List<InputTargetFilterData> targetFilters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner) {
		var inputPicker = AbilityTargetPickerFactory.CreateSingleTargetInputPicker();
		inputPicker.gridHighlighter = GridHighlighter.Instance;
		inputPicker.owner = owner;

		inputPicker.minRange = minRange;
		inputPicker.maxRange = maxRange;
		for(int i = 0; i < targetFilters.Count; i++)
			inputPicker.AddFilter(targetFilters[i].Create(owner));

		return inputPicker;
	}
}