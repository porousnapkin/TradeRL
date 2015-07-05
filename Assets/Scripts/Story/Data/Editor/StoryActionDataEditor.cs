using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(StoryActionData))]
public class StoryActionDataEditor : Editor {
	StoryActionData storyAction;
	List<Editor> successEditors = new List<Editor>();
	List<Editor> failEditors = new List<Editor>();

	public override void OnInspectorGUI() {
		storyAction = target as StoryActionData;

		storyAction.actionType = (StoryActionData.ActionType)EditorGUILayout.EnumPopup("Action Type", storyAction.actionType);

		if(storyAction.actionType == StoryActionData.ActionType.Skill)
			ShowSkillActionData();

		else if(storyAction.actionType == StoryActionData.ActionType.Immediate)
			ShowImmediateActionData();
	}

	void ShowSkillActionData() {
		storyAction.skillType = EditorGUILayout.TextField("skill", storyAction.skillType);
		ShowDescriptions();

		ShowEvents(storyAction.successEvents, successEditors, "Success Events");
		ShowEvents(storyAction.failEvents, failEditors, "Fail Events");
	}

	void ShowEvents(List<StoryActionEventData> events, List<Editor> editors, string eventNames) {
		EditorGUI.indentLevel++;
		int newSize = EditorGUILayout.IntField(eventNames, events.Count);
		EditorGUI.indentLevel++;

   		EditorHelper.UpdateList(ref events, newSize, () => null, DestroyAction);
    	EditorHelper.UpdateList(ref editors, newSize, () => null, (e) => {});
		for(int i = 0; i < events.Count; i++) {
			Editor thisEditor = editors[i];
			events[i] = EditorHelper.DisplayScriptableObjectWithEditor(storyAction, 
				events[i], ref thisEditor, "Type");
			editors[i] = thisEditor;
		}

		EditorGUI.indentLevel--;
		EditorGUI.indentLevel--;
	}

	void DestroyAction(StoryActionEventData data) {
		GameObject.DestroyImmediate(data, true);
    }

	void ShowDescriptions() {
		storyAction.shortDescription = EditorGUILayout.TextField("short description", storyAction.shortDescription);
		storyAction.longDescription = EditorGUILayout.TextField("long description", storyAction.longDescription);
	}

	void ShowImmediateActionData() {
		ShowDescriptions();

		ShowEvents(storyAction.successEvents, successEditors, "Events");
	}
}