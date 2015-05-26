using UnityEditor;
using UnityEngine;

public class DataHelper : EditorWindow {
	[MenuItem("Data/Create Map Data")]
	static public void CreateMapCreationData() {
		var data = ScriptableObject.CreateInstance<MapCreationData>();
		FinishCreation(data, "Assets/Data/MapData/NewMapData.asset");
	}	

	[MenuItem ("Data/Create Player Ability")]	
	public static void CreateNewAbilityData() {
		var data = ScriptableObject.CreateInstance<PlayerAbilityData>();
		FinishCreation(data, "Assets/Data/PlayerAbilities/NewAbilityData.asset");
	}

	[MenuItem ("Data/Create AI Character")]
	public static void CreateNewAICharacter() {
		var data = ScriptableObject.CreateInstance<AICharacterData>();
		FinishCreation(data, "Assets/Data/AICharacters/NewAICharacter.asset");
	}

	static void FinishCreation(ScriptableObject data, string path) {
		AssetDatabase.CreateAsset(data, "Assets/Data/AICharacters/NewAICharacter.asset");
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();

		Selection.objects = new Object[] { data };
		EditorGUIUtility.PingObject(data);
	}
}