using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(StatusEffectData))]
public class StatusEffectEditor : Editor
{
    Editor durationEditor;
    Editor actionEditor;

    public override void OnInspectorGUI()
    {
        var data = target as StatusEffectData;

        data.effectName = EditorGUILayout.TextField("name", data.effectName);
        data.description = EditorGUILayout.TextField("description", data.description);

        data.action = EditorHelper.DisplayScriptableObjectWithEditor(data, data.action, ref actionEditor, "Action");
        data.duration = EditorHelper.DisplayScriptableObjectWithEditor(data, data.duration, ref durationEditor,
            "Duration");

        EditorUtility.SetDirty(data);
    }
}

