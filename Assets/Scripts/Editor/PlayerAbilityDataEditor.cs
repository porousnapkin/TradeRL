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
    List<Editor> costsEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var abilityData = target as PlayerAbilityData;
        abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        EditorGUILayout.LabelField("Description");
        abilityData.description = EditorGUILayout.TextArea(abilityData.description);
        abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
        abilityData.targetPicker = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.targetPicker, ref targetPickerEditor, "Target Picker");
        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
        abilityData.animation = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.animation, ref animationEditor, "Animation");
        DisplayRestrictions(abilityData);
        DisplayCosts(abilityData);
        DesertEditorTools.DisplayLabelList(abilityData.labels, "Num Labels");

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

    private void DisplayCosts(PlayerAbilityData abilityData)
    {
        int newCount = EditorGUILayout.IntField("Num Costs", abilityData.costs.Count);
        EditorHelper.UpdateList(ref abilityData.costs, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
        EditorHelper.UpdateList(ref costsEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < abilityData.costs.Count; i++)
        {
            var cost = abilityData.costs[i];
            var editor = costsEditors[i];
            abilityData.costs[i] = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, cost, ref editor, "");
        }
        EditorGUI.indentLevel--;
    }
}