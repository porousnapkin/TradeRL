public class SelfPickerData : AbilityTargetPickerData {
	public override AbilityTargetPicker Create(Character owner) {
		var picker = new SelfTargetPicker();
		picker.owner = owner;

		return picker;
	}
}