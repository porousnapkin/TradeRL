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
}