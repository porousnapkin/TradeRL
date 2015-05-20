using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor {
	Editor costPickerEditor = null;
	Editor targetPickerEditor = null;
	Editor activatorEditor = null;

	public override void OnInspectorGUI() {
		var abilityData = target as AbilityData;
		abilityData.cost = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.cost, ref costPickerEditor, "Cost");
		abilityData.targetPicker = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.targetPicker, ref targetPickerEditor, "Target Picker");
		abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
    }
}