using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildingData))]
public class BuidlingDataEditor : Editor {
	Editor abilityEditor = null;

	public override void OnInspectorGUI() {
		var buildingData = target as BuildingData;
		buildingData.cost = EditorGUILayout.IntField("Cost", buildingData.cost);
		buildingData.ability = EditorHelper.DisplayScriptableObjectWithEditor(buildingData, buildingData.ability, ref abilityEditor, "Ability");
	}
}
