using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerAbilityData))]
public class PlayerAbilityDataEditor : Editor {
	Editor targetPickerEditor = null;
	Editor activatorEditor = null;
	Editor animationEditor = null;

	public override void OnInspectorGUI() {
		var abilityData = target as PlayerAbilityData;
		abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
		abilityData.targetPicker = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.targetPicker, ref targetPickerEditor, "Target Picker");
		abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
		abilityData.animation = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animation, ref animationEditor, "Animation");
		abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
    }
}