using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationData))]
public class LocationDataEditor : Editor {	
	LocationData locationData;

	public override void OnInspectorGUI() {
		locationData = target as LocationData;

		locationData.locationName = EditorGUILayout.TextField("Name", locationData.locationName);
		locationData.description = EditorGUILayout.TextField("Description", locationData.description);
		locationData.art = EditorGUILayout.ObjectField("Art", locationData.art, typeof(Sprite), false) as Sprite;
		locationData.activationType = (LocationType)EditorGUILayout.EnumPopup("Type", locationData.activationType);

		switch(locationData.activationType) {
		case LocationType.ConstantStory:
			ShowConstantStory(); break;
		case LocationType.OneOffStory:
			ShowOneOffStory(); break;
		case LocationType.ActiveStoryWithCooldown:
			ShowActiveStoryWithCooldown(); break;
		}

		EditorUtility.SetDirty(locationData);
		Editor.CreateEditor(locationData).serializedObject.ApplyModifiedProperties();
	}

	void ShowConstantStory() {
		locationData.firstStory = EditorGUILayout.ObjectField("Story", locationData.firstStory, typeof(StoryData), false) as StoryData;
	}

	void ShowOneOffStory() {
		locationData.firstStory = EditorGUILayout.ObjectField("Story", locationData.firstStory, typeof(StoryData), false) as StoryData;
		locationData.secondStory = EditorGUILayout.ObjectField("Deactive Story", locationData.secondStory, typeof(StoryData), false) as StoryData;
	}

	void ShowActiveStoryWithCooldown() {
		locationData.firstStory = EditorGUILayout.ObjectField("Story", locationData.firstStory, typeof(StoryData), false) as StoryData;
		locationData.secondStory = EditorGUILayout.ObjectField("Deactive Story", locationData.secondStory, typeof(StoryData), false) as StoryData;
		locationData.cooldownTurns = EditorGUILayout.IntField("Cooldown Turns", locationData.cooldownTurns);
	}
}