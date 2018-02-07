using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocationData))]
public class LocationDataEditor : Editor {	
	LocationData locationData;

	public override void OnInspectorGUI() {
		locationData = target as LocationData;

		locationData.locationName = EditorGUILayout.TextField("Name", locationData.locationName);
		locationData.art = EditorGUILayout.ObjectField("Art", locationData.art, typeof(Sprite), false) as Sprite;
	    locationData.randomlyPlace = EditorGUILayout.Toggle("Randomly Place", locationData.randomlyPlace);
	    locationData.hasGuard = EditorGUILayout.Toggle("Has Guard", locationData.hasGuard);
        if (locationData.hasGuard)
            locationData.guard = EditorGUILayout.ObjectField("Guard", locationData.guard, typeof(TravelingStoryData), false) as TravelingStoryData;

		ShowOneOffStory();

		EditorUtility.SetDirty(locationData);
		Editor.CreateEditor(locationData).serializedObject.ApplyModifiedProperties();
	}

	void ShowOneOffStory() {
		locationData.firstStory = EditorGUILayout.ObjectField("Story", locationData.firstStory, typeof(StoryData), false) as StoryData;
	}
}