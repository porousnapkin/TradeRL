using System.Collections.Generic;

public class SingleTargetInputPickerData : AbilityTargetPickerData {
	public int range = 1;
	public List<InputTargetFilterData> targetFilters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner) {
		var inputPicker = new SingleTargetInputPicker();
		inputPicker.inputCollector = GridInputCollector.Instance;
		inputPicker.gridHighlighter = GridHighlighter.Instance;
		inputPicker.owner = owner;

		inputPicker.range = range;
		for(int i = 0; i < targetFilters.Count; i++)
			inputPicker.AddFilter(targetFilters[0].Create(owner));

		return inputPicker;
	}
}