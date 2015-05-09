using UnityEngine;

public abstract class AbilityTargetPickerData : ScriptableObject {
	public abstract AbilityTargetPicker Create(Character owner);
}