using System;
using System.Collections.Generic;
using UnityEngine;

public class TravelingStoryData : ScriptableObject {
	public enum StepInAction {
		BeginStory,
		Combat,
        RandomEncounter
	}
    public bool use = true;
	public StepInAction stepInAction;
	public StoryData story;
	public CombatEncounterData combatData;
    public List<EncounterFaction> encounterFactions= new List<EncounterFaction>();
	public Sprite art;
	public string dataName;
	public string description;
	public string spawnMessage = "Stuff";
	public TravelingStoryAIData ai;
    public int stealthRating = 4;
    public float rarityDiscardChance = 1.0f;
    public AIAbilityData ambushAbility;
    public int encounterCost = 5;

    public TravelingStory Create(Vector2 position) {
		var travelingStory = DesertContext.StrangeNew<TravelingStoryImpl>();
		travelingStory.action = CreateAction();
		travelingStory.ai = ai.Create();
	    travelingStory.stealthRating = stealthRating;

		DesertContext.QuickBind<TravelingStoryMediated>(travelingStory);
		var travelingStoryGO = GameObject.Instantiate(PrefabGetter.travelingStoryPrefab);
		DesertContext.FinishQuickBind<TravelingStory>();

		travelingStoryGO.GetComponent<TravelingStoryVisuals>().Setup(art);
		travelingStory.TeleportToPosition(position);
		travelingStory.Setup();

		return travelingStory;
	}

	TravelingStoryAction CreateAction() {
        switch (stepInAction)
        {
            case StepInAction.BeginStory:
                return CreateStoryAction();
            case StepInAction.Combat:
                return CreateCombatAction();
            case StepInAction.RandomEncounter:
                return CreateRandomEncounter();
        }

		return CreateCombatAction();
	}

    TravelingStoryAction CreateRandomEncounter()
    {
        var action = DesertContext.StrangeNew<TravelingStoryBeginRandomEncounterAction>();
        action.encounterFactions = encounterFactions;
        action.ambushAbility = ambushAbility;
        action.encounterCost = encounterCost;
        return action;
    }

    TravelingStoryBeginStoryAction CreateStoryAction() {
		var storyAction = DesertContext.StrangeNew<TravelingStoryBeginStoryAction>();
		storyAction.story = story;
		return storyAction;
	}

	TravelingStoryBeginCombatAction CreateCombatAction() {
		var combatAction = DesertContext.StrangeNew<TravelingStoryBeginCombatAction>();
		combatAction.combatData = combatData;
		return combatAction;
	}
}
