using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(AICharacterData))]
public class AICharacterDataEditor : Editor {
	bool showStats = false;
	bool showActions = false;
	List<Editor> actionEditors = new List<Editor>();

	public override void OnInspectorGUI() {
		var data = target as AICharacterData;

		showStats = EditorGUILayout.Foldout(showStats, "Stats");
		if(showStats)
			ShowStats(data);

		showActions = EditorGUILayout.Foldout(showActions, "Actions");
		if(showActions)
			ShowActions(data);
    }

    void ShowStats(AICharacterData data) {
    	EditorGUI.indentLevel++;

    	data.displayName = EditorGUILayout.TextField("Display Name", data.displayName);
    	data.visuals = EditorGUILayout.ObjectField("Visuals", data.visuals, typeof(Sprite), false) as Sprite;
    	data.hp = EditorGUILayout.IntField("HP", data.hp);

    	EditorGUI.indentLevel--;
    }

    void ShowActions(AICharacterData data) {
    	EditorGUI.indentLevel++;

    	int newSize = EditorGUILayout.IntField("size", data.actions.Count);
   		EditorHelper.UpdateList(ref data.actions, newSize, () => null, DestroyAction);
    	EditorHelper.UpdateList(ref actionEditors, newSize, () => null, (e) => {});

    	EditorGUI.indentLevel++;

    	for(int i = 0 ; i < data.actions.Count; i++) {
    		var editor = actionEditors[i];
    		data.actions[i] = EditorHelper.DisplayScriptableObjectWithEditor(data, data.actions[i], ref editor, "Action");
    		actionEditors[i] = editor;
    	}

    	EditorGUI.indentLevel--;
    	EditorGUI.indentLevel--;
    }

    void DestroyAction(AIActionData data) {
		GameObject.DestroyImmediate(data, true);
    }
}