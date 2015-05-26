using UnityEditor;
using UnityEngine;

public class DataHelper : EditorWindow {
	[MenuItem("Data/Create Map Data")]
	static public void CreateMapCreationData() {
		var data = ScriptableObject.CreateInstance<MapCreationData>();
		FinishCreation(data, "Assets/Data/MapData/NewMapData.asset");
	}	

	[MenuItem ("Data/Create Player Ability")]	
	public static void CreateNewPlayerAbilityData() {
		var data = ScriptableObject.CreateInstance<PlayerAbilityData>();
		FinishCreation(data, "Assets/Data/PlayerAbilities/NewAbilityData.asset");
	}

	[MenuItem ("Data/Create AI Ability")]	
	public static void CreateNewAIAbilityData() {
		var data = ScriptableObject.CreateInstance<AIAbilityData>();
		FinishCreation(data, "Assets/Data/AIAbilities/NewAbilityData.asset");
	}

	[MenuItem ("Data/Create AI Character")]
	public static void CreateNewAICharacter() {
		var data = ScriptableObject.CreateInstance<AICharacterData>();
		FinishCreation(data, "Assets/Data/AICharacters/NewAICharacter.asset");
	}

	static void FinishCreation(ScriptableObject data, string path) {
		AssetDatabase.CreateAsset(data, path);
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();

		Selection.objects = new Object[] { data };
		EditorGUIUtility.PingObject(data);
	}
}