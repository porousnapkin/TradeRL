using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(StoryData))]
public class StoryDataEditor : Editor {
	bool displayActions = false;
	List<Editor> actionEditors = new List<Editor>();
	StoryData activeData;

	public override void OnInspectorGUI() {
		var storyData = target as StoryData;
		activeData = storyData;

		storyData.description = EditorGUILayout.TextField("Description", storyData.description);
		displayActions = EditorGUILayout.Foldout(displayActions, "Actions");
		if(displayActions)
			DisplayActions(storyData);
	}

	public void DisplayActions(StoryData data) {
    	EditorGUI.indentLevel++;

    	if(data.actions == null)
    		data.actions = new List<StoryActionData>();
		int newSize = EditorGUILayout.IntField("size", data.actions.Count);
   		EditorHelper.UpdateList(ref data.actions, newSize, CreateNewActionData, DestroyAction);
    	EditorHelper.UpdateList(ref actionEditors, newSize, () => null, (e) => {});

    	EditorGUI.indentLevel++;

    	for(int i = 0; i < actionEditors.Count; i++)
    		DisplayAction(i);

    	EditorGUI.indentLevel--;
    	EditorGUI.indentLevel--;
	}

	void DisplayAction(int i) {
		EditorGUILayout.LabelField((i + 1).ToString() + ".");
    	EditorGUI.indentLevel++;

		var ed = actionEditors[i];
		var action = activeData.actions[i];
		if(ed == null || ed.target != action)
			ed = Editor.CreateEditor(action);

		ed.OnInspectorGUI();

    	EditorGUI.indentLevel--;
	}

	StoryActionData CreateNewActionData() {
		var sad = ScriptableObject.CreateInstance<StoryActionData>();

		sad.hideFlags = HideFlags.HideInHierarchy;
		var assetPath = AssetDatabase.GetAssetPath(activeData);

		AssetDatabase.AddObjectToAsset(sad, assetPath);
		EditorUtility.SetDirty(activeData);
		EditorUtility.SetDirty(sad);
		AssetDatabase.SaveAssets();

		return sad;
	}

    void DestroyAction(StoryActionData data) {
		GameObject.DestroyImmediate(data, true);
    }
}