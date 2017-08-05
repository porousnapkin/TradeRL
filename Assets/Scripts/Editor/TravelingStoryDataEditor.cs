using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TravelingStoryData))]
public class TravelingStoryDataEditor : Editor {	
	TravelingStoryData data;

	public override void OnInspectorGUI() {
		data = target as TravelingStoryData;

        data.use = EditorGUILayout.Toggle("Use", data.use);
	    data.rarityDiscardChance = EditorGUILayout.FloatField("Rarity Discard Chance", data.rarityDiscardChance);
		data.dataName = EditorGUILayout.TextField("Name", data.dataName);
		data.description = EditorGUILayout.TextField("Description", data.description);
		data.spawnMessage = EditorGUILayout.TextField("Spawn Message", data.spawnMessage);
		data.art = EditorGUILayout.ObjectField("Art", data.art, typeof(Sprite), false) as Sprite;
		data.ai = EditorGUILayout.ObjectField("AI", data.ai, typeof(TravelingStoryAIData), false) as TravelingStoryAIData;
	    data.stealthRating = EditorGUILayout.IntField("Stealth Rating", data.stealthRating);
        data.difficulty = (EncounterDifficulty)EditorGUILayout.EnumPopup("Difficulty", data.difficulty);
		data.stepInAction = (TravelingStoryData.StepInAction)EditorGUILayout.EnumPopup("Step In Action", data.stepInAction);
		switch(data.stepInAction)
        {
            case TravelingStoryData.StepInAction.BeginStory:
                ShowBeginStory(); break;
            case TravelingStoryData.StepInAction.Combat:
                ShowCombat(); break;
            case TravelingStoryData.StepInAction.RandomEncounter:
                ShowRandomEncounter(); break;
        }

		Editor.CreateEditor(data).serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(data);
	}

    private void ShowRandomEncounter()
    {
        int newSize = EditorGUILayout.IntField("Factions to Pull From", data.encounterFactions.Count);
        EditorHelper.UpdateList(ref data.encounterFactions, newSize, () => EncounterFaction.Animal, (a) => { });
        EditorGUI.indentLevel++;
        for (int i = 0; i < newSize; i++)
            data.encounterFactions[i] = (EncounterFaction)(EditorGUILayout.EnumPopup(data.encounterFactions[i]));
        EditorGUI.indentLevel--;

        data.encounterCost = EditorGUILayout.IntField("Encounter Cost", data.encounterCost);
        data.ambushAbility = EditorGUILayout.ObjectField("Ambush", data.ambushAbility, typeof(AIAbilityData), false) as AIAbilityData;
    }

    void ShowBeginStory() {
		data.story = EditorGUILayout.ObjectField("Story", data.story, typeof(StoryData), false) as StoryData;
	}

	void ShowCombat() {
		data.combatData = EditorGUILayout.ObjectField("Combat", data.combatData, typeof(CombatEncounterData), false) as CombatEncounterData;
	}
}
