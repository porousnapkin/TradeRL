using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(StoryActionData))]
public class StoryActionDataEditor : Editor {
	StoryActionData storyAction;
	List<Editor> successEditors = new List<Editor>();
    List<Editor> restrictionEditors = new List<Editor>();

	public override void OnInspectorGUI() {
		storyAction = target as StoryActionData;

		storyAction.actionType = (StoryActionData.ActionType)EditorGUILayout.EnumPopup("Action Type", storyAction.actionType);
		ShowDescriptions();
	    ShowRestrictions();

		if(storyAction.actionType == StoryActionData.ActionType.Skill)
			ShowSkillActionData();

		else if(storyAction.actionType == StoryActionData.ActionType.Immediate)
			ShowImmediateActionData();

        EditorUtility.SetDirty(storyAction);
	}

	void ShowSkillActionData() {
		storyAction.skill = EditorGUILayout.ObjectField("Skill", storyAction.skill, typeof(SkillData), false) as SkillData;
		storyAction.difficulty = EditorGUILayout.IntField("Difficulty", storyAction.difficulty);

		ShowEvents(storyAction.successEvents, successEditors, "Action Events", ref storyAction.successMessage);
	}

	void ShowEvents(List<StoryActionEventData> events, List<Editor> editors, string eventNames, ref string message) {
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
	}

	void DestroyAction(StoryActionEventData data) {
		GameObject.DestroyImmediate(data, true);
    }

	void ShowDescriptions() {
		storyAction.storyDescription = EditorGUILayout.TextField("Story Description", storyAction.storyDescription);
		storyAction.gameplayDescription = EditorGUILayout.TextField("Gameplay Description", storyAction.gameplayDescription);
	}

    void ShowRestrictions()
    {
        int newSize = EditorGUILayout.IntField("Num Restrictions", storyAction.restrictions.Count);
        EditorGUI.indentLevel++;

        EditorHelper.UpdateList(ref storyAction.restrictions, newSize, () => null, DestroyRestriction);
        EditorHelper.UpdateList(ref restrictionEditors, newSize, () => null, (e) => { });
        for (int i = 0; i < newSize; i++)
        {
            Editor thisEditor = restrictionEditors[i];
            storyAction.restrictions[i] = EditorHelper.DisplayScriptableObjectWithEditor(storyAction,
                storyAction.restrictions[i], ref thisEditor, "Type");
            restrictionEditors[i] = thisEditor;
        }

        EditorGUI.indentLevel--;
    }

    void DestroyRestriction(RestrictionData data) {
		GameObject.DestroyImmediate(data, true);
    }

	void ShowImmediateActionData() {
		ShowEvents(storyAction.successEvents, successEditors, "Events", ref storyAction.successMessage);
	}
}