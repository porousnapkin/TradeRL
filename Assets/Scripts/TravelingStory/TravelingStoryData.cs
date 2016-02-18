using UnityEngine;

public class TravelingStoryData : ScriptableObject {
	public enum StepInAction {
		BeginStory,
		Combat,
	}
	public StepInAction stepInAction;
	public StoryData story;
	public CombatEncounterData combatData;
	public Sprite art;
	public string dataName;
	public string description;
	public TravelingStoryAIData ai;

	public void Create(Vector2 position) {
		var travelingStory = DesertContext.StrangeNew<TravelingStory>();
		travelingStory.action = CreateAction();
		travelingStory.ai = ai.Create();

		DesertContext.QuickBind(travelingStory);
		var travelingStoryGO = GameObject.Instantiate(PrefabGetter.travelingStoryPrefab);
		DesertContext.FinishQuickBind<TravelingStory>();

		travelingStoryGO.GetComponent<TravelingStoryVisuals>().Setup(art);
		travelingStory.TeleportToPosition(position);
	}

	TravelingStoryAction CreateAction() {
		switch(stepInAction) {
		case StepInAction.BeginStory:
			return CreateStoryAction();		
		case StepInAction.Combat:
			return CreateCombatAction();
		}

		return CreateCombatAction();
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
