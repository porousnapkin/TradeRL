using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(CombatAINodeData))]
public class CombatAINodeDataEditor : Editor
{
    List<Editor> conditionalsEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var data = target as CombatAINodeData;

        int newSize = EditorGUILayout.IntField("num conditionals", data.conditionals.Count);
   		EditorHelper.UpdateList(ref data.conditionals, newSize, () => null, (n) => GameObject.DestroyImmediate(n, true));
    	EditorHelper.UpdateList(ref conditionalsEditors, newSize, () => null, (e) => {});

        EditorGUI.indentLevel++;

        for (int i = 0; i < data.conditionals.Count; i++)
        {
            var editor = conditionalsEditors[i];
            data.conditionals[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, data.conditionals[i], ref editor, "Conditionals");
            conditionalsEditors[i] = editor;
        }

        EditorGUI.indentLevel--;

        newSize = EditorGUILayout.IntField("num abilities", data.abilities.Count);
   		EditorHelper.UpdateList(ref data.abilities, newSize, () => null, (a) => {});

        EditorGUI.indentLevel++;

        for(int i = 0; i < data.abilities.Count; i++)
            data.abilities[i] = EditorGUILayout.ObjectField(data.abilities[i], typeof(AIAbilityData), false) as AIAbilityData;

        EditorGUI.indentLevel--;
    }
}
