using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MultiActivatorAbilityData))]
public class MultiActivatorAbilityDataEditor : Editor
{
    List<Editor> abilityEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var data = target as MultiActivatorAbilityData;

        int newCount = EditorGUILayout.IntField("Num Abilities", data.abilityActivators.Count);
        EditorHelper.UpdateList(ref data.abilityActivators, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
        EditorHelper.UpdateList(ref abilityEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < data.abilityActivators.Count; i++)
        {
            var activator = data.abilityActivators[i];
            var editor = abilityEditors[i];
            data.abilityActivators[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, activator, ref editor, "");
        }
        EditorGUI.indentLevel--;
    }
}

