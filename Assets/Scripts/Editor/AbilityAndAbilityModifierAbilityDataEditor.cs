using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityAndAbilityModifierAbilityData))]
public class AbilityAndAbilityModifierAbilityDataEditor : Editor
{
    List<Editor> modifierEditors = new List<Editor>();
    Editor abilityEditor = null;

    public override void OnInspectorGUI()
    {
        var data = target as AbilityAndAbilityModifierAbilityData;

        int newCount = EditorGUILayout.IntField("Num modifiers", data.abilityModifiers.Count);
        EditorHelper.UpdateList(ref data.abilityModifiers, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
        EditorHelper.UpdateList(ref modifierEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < data.abilityModifiers.Count; i++)
        {
            var activator = data.abilityModifiers[i];
            var editor = modifierEditors[i];
            data.abilityModifiers[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, activator, ref editor, "");
        }

        EditorUtility.SetDirty(data);

        EditorGUI.indentLevel--;

        data.abilityActivator = EditorHelper.DisplayScriptableObjectWithEditor(data, data.abilityActivator, ref abilityEditor, "Ability");
    }
}
