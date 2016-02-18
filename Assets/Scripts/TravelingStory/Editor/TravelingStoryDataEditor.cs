using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TravelingStoryData))]
public class TravelingStoryDataEditor : Editor {	
	TravelingStoryData data;

	public override void OnInspectorGUI() {
		data = target as TravelingStoryData;

		data.dataName = EditorGUILayout.TextField("Name", data.dataName);
		data.description = EditorGUILayout.TextField("Description", data.description);
		data.art = EditorGUILayout.ObjectField("Art", data.art, typeof(Sprite), false) as Sprite;
		data.ai = EditorGUILayout.ObjectField("AI", data.ai, typeof(TravelingStoryAIData), false) as TravelingStoryAIData;
		data.stepInAction = (TravelingStoryData.StepInAction)EditorGUILayout.EnumPopup("Step In Action", data.stepInAction);
		switch(data.stepInAction) {
		case TravelingStoryData.StepInAction.BeginStory:
			ShowBeginStory(); break;
		case TravelingStoryData.StepInAction.Combat:
			ShowCombat(); break;
		}

		Editor.CreateEditor(data).serializedObject.ApplyModifiedProperties();
	}

	void ShowBeginStory() {
		data.story = EditorGUILayout.ObjectField("Story", data.story, typeof(StoryData), false) as StoryData;
	}

	void ShowCombat() {
		data.combatData = EditorGUILayout.ObjectField("Combat", data.combatData, typeof(CombatEncounterData), false) as CombatEncounterData;
	}
}
