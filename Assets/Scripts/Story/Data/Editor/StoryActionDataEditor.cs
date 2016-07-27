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
		storyAction.skill = EditorGUILayout.ObjectField("Skill", storyAction.skill, typeof(SkillData), false) as SkillData;
		storyAction.difficulty = EditorGUILayout.IntField("Difficulty", storyAction.difficulty);
		ShowDescriptions();

		ShowEvents(storyAction.successEvents, successEditors, "Success Events", ref storyAction.successMessage);
		ShowEvents(storyAction.failEvents, failEditors, "Fail Events", ref storyAction.failedMessage);
	}

	void ShowEvents(List<StoryActionEventData> events, List<Editor> editors, string eventNames, ref string message) {
		EditorGUI.indentLevel++;
		int newSize = EditorGUILayout.IntField(eventNames, events.Count);
		message = EditorGUILayout.TextField("Message", message);
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
		storyAction.storyDescription = EditorGUILayout.TextField("Story Description", storyAction.storyDescription);
		storyAction.gameplayDescription = EditorGUILayout.TextField("Gameplay Description", storyAction.gameplayDescription);
	}

	void ShowImmediateActionData() {
		ShowDescriptions();

		ShowEvents(storyAction.successEvents, successEditors, "Events", ref storyAction.successMessage);
	}
}