using System.Collections.Generic;

public class MultiTargetPickerData : AbilityTargetPickerData
{
	public List<InputTargetFilterData> filters = new List<InputTargetFilterData>();

    public override AbilityTargetPicker Create(Character owner)
    {
        var inputPicker = DesertContext.StrangeNew<MultiTargetPicker>();

        inputPicker.filters = filters.ConvertAll(fd => fd.Create(owner));

        return inputPicker;
    }
}

