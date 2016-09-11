using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(PlayerAbilityModifierData))]
public class PlayerAbilityModifierDataEditor : Editor
{
	Editor abilityEditor = null;
    List<Editor> costsEditors = new List<Editor>();

	public override void OnInspectorGUI()
	{
		var abilityData = target as PlayerAbilityModifierData;
		abilityData.cooldown = EditorGUILayout.IntField("Cooldown", abilityData.cooldown);
		abilityData.abilityModifier = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.abilityModifier, ref abilityEditor, "Ability Modifier");
		abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        DisplayCosts(abilityData);

		EditorUtility.SetDirty(abilityData);
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