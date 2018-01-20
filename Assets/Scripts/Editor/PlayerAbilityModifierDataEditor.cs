using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(PlayerAbilityModifierData))]
public class PlayerAbilityModifierDataEditor : Editor
{
	Editor abilityEditor = null;
    List<Editor> costsEditors = new List<Editor>();
	Editor targetPickerEditor = null;

	public override void OnInspectorGUI()
    {
        var abilityData = target as PlayerAbilityModifierData;
        abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        abilityData.description = EditorGUILayout.TextField("Description", abilityData.description);
        abilityData.initiativeMod = EditorGUILayout.IntField("Initiative Mod", abilityData.initiativeMod);
        abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
        abilityData.abilityModifier = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.abilityModifier, ref abilityEditor, "Ability Modifier");
        abilityData.usesAbilitysTargets = EditorGUILayout.Toggle("Use Ability's Targets", abilityData.usesAbilitysTargets);
        if (!abilityData.usesAbilitysTargets)
            DisplayAITargeter(abilityData);

        DisplayCosts(abilityData);
        abilityData.hasLabelRequirements = EditorGUILayout.Toggle("Has Label Requirements", abilityData.hasLabelRequirements);
        if(abilityData.hasLabelRequirements)
            DesertEditorTools.DisplayLabelList(abilityData.labelRequirements, "Num Label Requirements");
        DesertEditorTools.DisplayLabelList(abilityData.labels, "Num Labels");

        EditorUtility.SetDirty(abilityData);
    }

    private void DisplayAITargeter(PlayerAbilityModifierData abilityData)
    {
        EditorGUI.indentLevel++;
        EditorGUILayout.LabelField("Target Picker");
        abilityData.targetPicker = EditorHelper.CreateAndDisplaySpecificScriptableObjectType(abilityData.targetPicker, abilityData, ref targetPickerEditor);
        EditorGUI.indentLevel--;
    }

    private static void DisplayLabels(PlayerAbilityModifierData abilityData)
    {
        int newCount = EditorGUILayout.IntField("Num Labels", abilityData.labelRequirements.Count);
        EditorHelper.UpdateList(ref abilityData.labelRequirements, newCount, () => AbilityLabel.Attack, (a) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < abilityData.labelRequirements.Count; i++)
            abilityData.labelRequirements[i] = (AbilityLabel)EditorGUILayout.EnumPopup(abilityData.labelRequirements[i]);
        EditorGUI.indentLevel--;
    }

    private void DisplayCosts(PlayerAbilityModifierData abilityData)
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