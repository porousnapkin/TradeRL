using System.Collections.Generic;

public class SingleTargetInputPickerData : AbilityTargetPickerData {
	public int minRange = 1;
	public int maxRange = 1;
	public List<InputTargetFilterData> filters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner) {
		var inputPicker = DesertContext.StrangeNew<SingleTargetInputPicker>();

        inputPicker.filters = filters.ConvertAll(fd => fd.Create(owner));

		return inputPicker;
	}
}