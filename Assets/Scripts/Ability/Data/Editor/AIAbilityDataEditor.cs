using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIAbilityData))]
public class AIAbilityDataEditor : Editor {
	Editor targetPickerEditor = null;
	Editor activatorEditor = null;
	Editor animationEditor = null;

	public override void OnInspectorGUI() {
		var abilityData = target as AIAbilityData;
		abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);

        EditorGUILayout.LabelField("Target Picker");
        EditorGUI.indentLevel++;
        abilityData.targetPicker = EditorHelper.CreateAndDisplaySpecificScriptableObjectType(abilityData.targetPicker, abilityData, ref targetPickerEditor);
        EditorGUI.indentLevel--;

		abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
		abilityData.animation = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animation, ref animationEditor, "Animation");

        EditorUtility.SetDirty(abilityData);
    }
}