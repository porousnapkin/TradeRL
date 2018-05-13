using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TravelingStoryFactory {
    [Inject] public RandomEncounterGenerator encounterGenerator { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }
    [Inject] public MapTownRegistry mapTownRegistry { private get; set; }
    Sprite spriteToUse;
	List<TravelingStoryData> travelingStories;
    Transform parent;

    [PostConstruct]
	public void PostConstruct() {
		travelingStories = Resources.LoadAll<TravelingStoryData>("TravelingStory").ToList();
        travelingStories.RemoveAll(t => !t.use);

        parent = new GameObject("TravelingStories").transform;
	}

    public TravelingStory Create(Vector2 position)
    {
        return CreateSpecificStory(position, GetDataToSpawn(position));
    }

    public TravelingStory CreateSpecificStory(Vector2 position, TravelingStoryData data, TravelingStoryAIData aiOverride = null)
    {
        spriteToUse = data.art;

        var travelingStory = DesertContext.StrangeNew<TravelingStoryController>();
        travelingStory.PopupText = "<u>" + data.dataName + "</u>\n" + data.difficulty + " difficulty";
        travelingStory.action = CreateAction(data);
        if (aiOverride != null)
            travelingStory.ai = aiOverride.Create();
        else
            travelingStory.ai = data.ai.Create();
        travelingStory.stealthRating = data.stealthRating;

        DesertContext.QuickBind<TravelingStoryMediated>(travelingStory);
        var travelingStoryGO = GameObject.Instantiate(PrefabGetter.travelingStoryPrefab, parent);
        DesertContext.FinishQuickBind<TravelingStoryMediated>();

        travelingStoryGO.GetComponent<TravelingStoryVisuals>().Setup(spriteToUse);
        travelingStory.TeleportToPosition(position);
        travelingStory.Setup();

        return travelingStory;
    }

    TravelingStoryData GetDataToSpawn(Vector2 position)
    {
        var town = mapTownRegistry.GetTownForPosition(position);
        TravelingStoryData possibleStory;

        //generic story
        if (town == null)
            possibleStory = travelingStories[Random.Range(0, travelingStories.Count)];
        //town specific story
        else
            possibleStory = town.GetRandomNearbyEncounter();

        if (Random.value < possibleStory.rarityDiscardChance)
            return possibleStory;
        else
            return GetDataToSpawn(position);
    }

    TravelingStoryAction CreateAction(TravelingStoryData data)
    {
        switch (data.stepInAction)
        {
            case TravelingStoryData.StepInAction.BeginStory:
                return CreateStoryAction(data);
            case TravelingStoryData.StepInAction.Combat:
                return CreateCombatAction(data);
            case TravelingStoryData.StepInAction.RandomEncounter:
                return CreateRandomEncounter(data);
        }

        return CreateCombatAction(data);
    }

    TravelingStoryAction CreateRandomEncounter(TravelingStoryData data)
    {
        var combatData = ScriptableObject.CreateInstance<CombatEncounterData>();
        combatData.characters = encounterGenerator.CreateEncounterGroupForFactions(data.encounterFactions, data.encounterCost);
        combatData.ambushAbility = data.ambushAbility;
        var combatAction = DesertContext.StrangeNew<TravelingStoryBeginCombatAction>();
        combatAction.combatData = combatData;

        spriteToUse = combatData.characters[Random.Range(0, combatData.characters.Count)].visuals;
        return combatAction;
    }

    TravelingStoryBeginStoryAction CreateStoryAction(TravelingStoryData data)
    {
        var storyAction = DesertContext.StrangeNew<TravelingStoryBeginStoryAction>();
        storyAction.story = data.story;
        return storyAction;
    }

    TravelingStoryBeginCombatAction CreateCombatAction(TravelingStoryData data)
    {
        var combatAction = DesertContext.StrangeNew<TravelingStoryBeginCombatAction>();
        combatAction.combatData = data.combatData;
        return combatAction;
    }
}
