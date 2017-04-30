﻿using UnityEngine;

public class TravelingStoryFactory {
    [Inject] public RandomEncounterGenerator encounterGenerator { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }
    Sprite spriteToUse;

    public TravelingStory Create(Vector2 position, TravelingStoryData data)
    {
        spriteToUse = data.art;

        var travelingStory = DesertContext.StrangeNew<TravelingStoryController>();
        travelingStory.action = CreateAction(data);
        travelingStory.ai = data.ai.Create();
        travelingStory.stealthRating = data.stealthRating;

        DesertContext.QuickBind<TravelingStoryMediated>(travelingStory);
        var travelingStoryGO = GameObject.Instantiate(PrefabGetter.travelingStoryPrefab);
        DesertContext.FinishQuickBind<TravelingStory>();

        travelingStoryGO.GetComponent<TravelingStoryVisuals>().Setup(spriteToUse);
        travelingStory.TeleportToPosition(position);
        travelingStory.Setup();

        return travelingStory;
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
