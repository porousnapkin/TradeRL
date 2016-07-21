using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PlayerAbilityData))]
public class PlayerAbilityDataEditor : Editor
{
    Editor targetPickerEditor = null;
    Editor activatorEditor = null;
    Editor animationEditor = null;
    List<Editor> restrictionEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var abilityData = target as PlayerAbilityData;
        abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
        abilityData.effortCost = EditorGUILayout.IntField("Effort", abilityData.effortCost);
        abilityData.targetPicker = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.targetPicker, ref targetPickerEditor, "Target Picker");
        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
        abilityData.animation = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animation, ref animationEditor, "Animation");
        abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        DisplayRestrictions(abilityData);

		EditorUtility.SetDirty(abilityData);
    }

    private void DisplayRestrictions(PlayerAbilityData abilityData)
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