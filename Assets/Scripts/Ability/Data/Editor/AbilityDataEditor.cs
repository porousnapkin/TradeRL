using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityData))]
public class AbilityDataEditor : Editor {
	Editor costPickerEditor = null;
	Editor targetPickerEditor = null;
	Editor activatorEditor = null;

	public override void OnInspectorGUI() {
		var abilityData = target as AbilityData;
		abilityData.cost = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.cost, costPickerEditor, "Cost");
		abilityData.targetPicker = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.targetPicker, targetPickerEditor, "Target Picker");
		abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, activatorEditor, "Activator");
    }
}