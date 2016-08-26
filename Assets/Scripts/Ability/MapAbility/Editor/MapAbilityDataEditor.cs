using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(MapAbilityData))]
public class MapAbilityDataEditor : Editor
{
    Editor activatorEditor = null;
    List<Editor> restrictionEditors = new List<Editor>();
    List<Editor> costsEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var abilityData = target as MapAbilityData;
        abilityData.activator = EditorHelper.DisplayScriptableObjectWithEditor(abilityData, abilityData.activator, ref activatorEditor, "Activator");
        abilityData.abilityName = EditorGUILayout.TextField("Name", abilityData.abilityName);
        DisplayRestrictions(abilityData);
        DisplayCosts(abilityData);

        EditorUtility.SetDirty(abilityData);
    }

    private void DisplayRestrictions(MapAbilityData abilityData)
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

    private void DisplayCosts(MapAbilityData abilityData)
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
