using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AIAbilityData))]
public class AIAbilityDataEditor : Editor {
	Editor targetPickerEditor = null;
	Editor activatorEditor = null;
	Editor animationEditor = null;
    List<Editor> restrictionEditors = new List<Editor>();

	public override void OnInspectorGUI()
    {
        var abilityData = target as AIAbilityData;
        abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);

        EditorGUILayout.LabelField("Target Picker");
        EditorGUI.indentLevel++;
        abilityData.targetPicker = EditorHelper.CreateAndDisplaySpecificScriptableObjectType(abilityData.targetPicker, abilityData, ref targetPickerEditor);
        EditorGUI.indentLevel--;

        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
        abilityData.animation = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animation, ref animationEditor, "Animation");
        DisplayRestrictions(abilityData);

        int newCount = EditorGUILayout.IntField("Num Labels", abilityData.labels.Count);
        EditorHelper.UpdateList(ref abilityData.labels, newCount, () => AbilityLabel.Attack, (t) => {});
        EditorGUI.indentLevel++;
        for (int i = 0; i < abilityData.labels.Count; i++)
            abilityData.labels[i] = (AbilityLabel)EditorGUILayout.EnumPopup(abilityData.labels[i]);
        EditorGUI.indentLevel--;

        EditorUtility.SetDirty(abilityData);
    }

    private void DisplayRestrictions(AIAbilityData abilityData)
    {
        int newCount = EditorGUILayout.IntField("Num Restrictions", abilityData.restrictions.Count);
        EditorHelper.UpdateList(ref abilityData.restrictions, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
        EditorHelper.UpdateList(ref restrictionEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < abilityData.restrictions.Count; i++)
        {
            var restriction = abilityData.restrictions[i];
            var editor = restrictionEditors[i];
            abilityData.restrictions[i] = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, restriction, ref editor, "");
        }
        EditorGUI.indentLevel--;
    }
}