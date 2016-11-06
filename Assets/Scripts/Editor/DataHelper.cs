using UnityEditor;
using UnityEngine;

public class DataHelper : EditorWindow {
    [MenuItem("Data/Character Creation/Create Character Creation Data")]
    static public void CreateCharacterCreationData()
    {
        var data = ScriptableObject.CreateInstance<CharacterCreationDataBlob>();
        FinishCreation(data, "Assets/Data/CharacterCreation/NewCharacterCreationData.asset");
    }

    [MenuItem("Data/Character Creation/Create Premade Character Data")]
    static public void CreatePremadeCharacterData()
    {
        var data = ScriptableObject.CreateInstance<PremadeCharacterData>();
        FinishCreation(data, "Assets/Data/CharacterCreation/Premades/NewPremade.asset");
    }

	[MenuItem("Data/Create Map Data")]
	static public void CreateMapCreationData() {
		var data = ScriptableObject.CreateInstance<MapCreationData>();
		FinishCreation(data, "Assets/Data/MapData/NewMapData.asset");
	}

    [MenuItem("Data/Create Status Effect")]
    static public void CreateStatusEffect()
    {
        var data = ScriptableObject.CreateInstance<StatusEffectData>();
		FinishCreation(data, "Assets/Data/StatusEffects/NewStatusEffect.asset");
    }

    [MenuItem("Data/Create Map Ability")]
    public static void CreateMapAbilityData()
    {
        var data = ScriptableObject.CreateInstance<MapAbilityData>();
		FinishCreation(data, "Assets/Data/MapAbilities/NewMapAbilityData.asset");
    }

    [MenuItem("Data/Create Item")]
    public static void CreateItem()
    {
        var data = ScriptableObject.CreateInstance<ItemData>();
        FinishCreation(data, "Assets/Data/Items/NewItem.asset");
    }

    [MenuItem ("Data/Combat/Create Player Ability")]	
	public static void CreateNewPlayerAbilityData() {
		var data = ScriptableObject.CreateInstance<PlayerAbilityData>();
		FinishCreation(data, "Assets/Data/PlayerAbilities/NewAbilityData.asset");
	}

	[MenuItem ("Data/Combat/Create Player Ability Modifier")]
	public static void CreateNewPlayerAbilityModifierData() {
		var data = ScriptableObject.CreateInstance<PlayerAbilityModifierData>();
		FinishCreation(data, "Assets/Data/PlayerAbilityModifiers/NewModifierData.asset");
	}

	[MenuItem ("Data/Combat/Create AI Ability")]	
	public static void CreateNewAIAbilityData() {
		var data = ScriptableObject.CreateInstance<AIAbilityData>();
		FinishCreation(data, "Assets/Data/AIAbilities/NewAbilityData.asset");
	}

    [MenuItem ("Data/Combat/Create Combat AI")]
    public static void CreateNewCombatAIData()
    {
		var data = ScriptableObject.CreateInstance<CombatAIData>();
		FinishCreation(data, "Assets/Data/CombatAIs/NewCombatAI.asset");
    }

	[MenuItem ("Data/Combat/Create AI Character")]
	public static void CreateNewAICharacter() {
		var data = ScriptableObject.CreateInstance<AICharacterData>();
		FinishCreation(data, "Assets/Data/AICharacters/NewAICharacter.asset");
	}

	[MenuItem ("Data/Combat/Create Combat Encounter")]
	public static void CreateNewCombatEncounter() {
		var data = ScriptableObject.CreateInstance<CombatEncounterData>();
		FinishCreation(data, "Assets/Data/Encounters/Combat/NewCombatEncounter.asset");
	}

	[MenuItem ("Data/Story/Create Story")]
	public static void CreateStory() {
		var data = ScriptableObject.CreateInstance<StoryData>();
		FinishCreation(data, "Assets/Data/Stories/NewStory.asset");
	}

	[MenuItem("Data/Character Creation/Create Skill")]
	public static void CreateSkill() {
		var data = ScriptableObject.CreateInstance<SkillData>();
		FinishCreation(data, "Assets/Data/Skills/Skill.asset");
	}

	[MenuItem("Data/Story/Create Location")]
	public static void CreateLocation() {
		var data = ScriptableObject.CreateInstance<LocationData>();
		FinishCreation(data, "Assets/Data/Resources/Locations/Location.asset");
	}

	[MenuItem("Data/Story/Create Traveling Story")]
	public static void CreateTravelingStory() {
		var data = ScriptableObject.CreateInstance<TravelingStoryData>();
		FinishCreation(data, "Assets/Data/Resources/TravelingStory/TravelingStory.asset");
	}

	[MenuItem("Data/Story/AI Routines/Chase")]
	public static void CreateTravelingStoryChase() {
		var data = ScriptableObject.CreateInstance<TravelingStoryChaseData>();
		FinishCreation(data, "Assets/Data/Resources/TravelingStory/AI/Routines/TravelingStoryAIRoutineData.asset");
	}

	[MenuItem("Data/Story/AI Routines/Wander")]
	public static void CreateTravelingStoryWander() {
		var data = ScriptableObject.CreateInstance<TravelingStoryWanderData>();
		FinishCreation(data, "Assets/Data/Resources/TravelingStory/AI/Routines/TravelingStoryAIRoutineData.asset");
	}

	[MenuItem("Data/Story/AI Routines/Flee")]
	public static void CreateTravelingStoryFlee() {
		var data = ScriptableObject.CreateInstance<TravelingStoryFleeData>();
		FinishCreation(data, "Assets/Data/Resources/TravelingStory/AI/Routines/TravelingStoryAIRoutineData.asset");
	}

	[MenuItem("Data/Story/Create Traveling Story AI")]
	public static void CreateTravelingStoryAI() {
		var data = ScriptableObject.CreateInstance<TravelingStoryAIData>();
		FinishCreation(data, "Assets/Data/Resources/TravelingStory/AI/TravelingStoryAIData.asset");
	}

	[MenuItem("Data/City/Create City Action Data")]
	public static void CreateCityActionData() {
		var data = ScriptableObject.CreateInstance<CityActionData>();
		FinishCreation(data, "Assets/Data/Resources/CityActions/CityActionData.asset");
	}

	[MenuItem("Data/City/Create Building Data")]
	public static void CreateBuildingData() {
		var data = ScriptableObject.CreateInstance<BuildingData>();
		FinishCreation(data, "Assets/Data/Resources/Buildings/BuildingData.asset");
	}

	static void FinishCreation(ScriptableObject data, string path) {
		AssetDatabase.CreateAsset(data, path);
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();

		Selection.objects = new Object[] { data };
		EditorGUIUtility.PingObject(data);
	}
}