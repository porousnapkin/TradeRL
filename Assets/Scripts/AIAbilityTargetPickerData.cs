using System.Collections.Generic;

public class AIAbilityTargetPickerData : AbilityTargetPickerData
{
    public List<InputTargetFilterData> filters = new List<InputTargetFilterData>();

	public override AbilityTargetPicker Create(Character owner)
    {
        var targetPicker = DesertContext.StrangeNew<AIAbilityTargetPicker>();

        targetPicker.filters = filters.ConvertAll(fd => fd.Create(owner));

        return targetPicker;
    }
}
