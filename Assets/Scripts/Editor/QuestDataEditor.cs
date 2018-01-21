using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestData))]
public class QuestDataEditor : Editor
{
    List<Editor> eventEditors = new List<Editor>();
    QuestData activeData;

    public override void OnInspectorGUI()
    {
        var questData = target as QuestData;
        activeData = questData;

        questData.title = EditorGUILayout.TextField("Title", questData.title);
        questData.description = EditorGUILayout.TextField("Description", questData.description);
        ShowEvents(questData.successEvents, eventEditors, "success events");

        EditorUtility.SetDirty(questData);
    }

    void ShowEvents(List<StoryActionEventData> events, List<Editor> editors, string eventNames)
    {
        int newSize = EditorGUILayout.IntField(eventNames, events.Count);
        EditorGUI.indentLevel++;

        EditorHelper.UpdateList(ref events, newSize, () => null, DestroyAction);
        EditorHelper.UpdateList(ref editors, newSize, () => null, (e) => { });
        for (int i = 0; i < events.Count; i++)
        {
            Editor thisEditor = editors[i];
            events[i] = EditorHelper.DisplayScriptableObjectWithEditor(activeData,
                events[i], ref thisEditor, "Type");
            editors[i] = thisEditor;
        }

        EditorGUI.indentLevel--;
    }

    void DestroyAction(StoryActionEventData data)
    {
        GameObject.DestroyImmediate(data, true);
    }
}

