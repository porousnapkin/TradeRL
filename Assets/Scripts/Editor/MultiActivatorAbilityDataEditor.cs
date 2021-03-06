using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MultiAnimationData))]
public class MultiAnimationDataEditor :Editor
{
    List<Editor> abilityEditors = new List<Editor>();

    public override void OnInspectorGUI()
    {
        var data = target as MultiAnimationData;

        data.howToPlay = (MultiAnimation.HowToPlay)EditorGUILayout.EnumPopup("How To Play", data.howToPlay);
        int newCount = EditorGUILayout.IntField("Num Animations", data.animations.Count);
        EditorHelper.UpdateList(ref data.animations, newCount, () => null, (t) => GameObject.DestroyImmediate(t));
        EditorHelper.UpdateList(ref abilityEditors, newCount, () => null, (t) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < data.animations.Count; i++)
        {
            var activator = data.animations[i];
            var editor = abilityEditors[i];
            data.animations[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, activator, ref editor, "");
        }

        EditorUtility.SetDirty(data);

        EditorGUI.indentLevel--;
    }
}

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

        EditorUtility.SetDirty(data);

        EditorGUI.indentLevel--;
    }
}

