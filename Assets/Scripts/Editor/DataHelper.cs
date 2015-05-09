using UnityEditor;
using UnityEngine;

public class DataHelper : EditorWindow {
	[MenuItem("Data/Create Map Data")]
	static public void CreateMapCreationData() {
		var data = ScriptableObject.CreateInstance<MapCreationData>();
		AssetDatabase.CreateAsset(data, "Assets/Data/MapData/NewMapData.asset");
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();

		Selection.objects = new Object[] { data };
		EditorGUIUtility.PingObject(data);
	}	

	[MenuItem ("Data/Create Ability Data")]	
	public static void CreateNewAbilityData() {
		var data = ScriptableObject.CreateInstance<AbilityData>();
		AssetDatabase.CreateAsset(data, "Assets/Data/AbilityData/NewAbilityData.asset");
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();

		Selection.objects = new Object[] { data };
		EditorGUIUtility.PingObject(data);	
	}
}