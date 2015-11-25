using UnityEngine;

public class StoryFactory {
	public static Effort effort;
	public static GameObject storyVisualsPrefab;
	public static Transform storyCanvas;
	public static GameObject skillStoryActionPrefab;
	public static GameObject storyActionPrefab;
	public static PlayerController playerController;
	public static PlayerSkills playerSkills;
	public static Inventory inventory;

	public static StoryVisuals CreateStory(StoryData sd, System.Action finishedAction) {
		var visuals = CreateStoryVisuals();
		visuals.Setup (sd.title, sd.description);
		visuals.storyFinishedEvent += finishedAction;
		
		foreach(var sad in sd.actions)
			visuals.AddAction(CreateSkillStoryActionVisualsFromData(sad, () => visuals.Finished()));

		return visuals;
	}

	static GameObject CreateSkillStoryActionVisualsFromData(StoryActionData sad, System.Action finishedAction) {
		if(sad.actionType == StoryActionData.ActionType.Skill)
			return CreateSkillStoryActionVisuals(CreateSkillStoryAction(sad), finishedAction);
		else
			return CreateStoryActionVisuals(sad, finishedAction);
	}
	
	static GameObject CreateSkillStoryActionVisuals(SkillStoryAction a, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(skillStoryActionPrefab) as GameObject;
		actionGO.GetComponent<SkillStoryActionVisuals>().Setup(a);
		actionGO.GetComponent<SkillStoryActionVisuals>().FinishedEvent += finishedAction;
		
		return actionGO;
	}
	
	static SkillStoryAction CreateSkillStoryAction(StoryActionData actionData) {
		var sa = new SkillStoryAction();
		sa.effort = effort;
		
		sa.chanceSuccess = CalculateChanceOfSuccess(actionData);
		sa.effortToSurpass = CalculateEffort(actionData);
		sa.storyDescription = actionData.storyDescription;
		sa.gameDescription = actionData.gameplayDescription;
		sa.successEvents = actionData.successEvents.ConvertAll(ae => ae.Create());
		sa.failEvents = actionData.failEvents.ConvertAll(ae => ae.Create());
		
		return sa;
	}
	
	static float CalculateChanceOfSuccess(StoryActionData actionData) {
		float chanceOffset = 0.0f;
		var skillLevel = playerSkills.GetSkillLevel(actionData.skill);
		if(skillLevel == 0)
			chanceOffset = 0.4f;
		var difference = actionData.difficulty - skillLevel;
		chanceOffset += 0.2f * difference;
		
		return Mathf.Max (0.1f, 0.9f - chanceOffset);
	}
	
	static int CalculateEffort(StoryActionData actionData) {
		int effort = 0;
		var skillLevel = playerSkills.GetSkillLevel(actionData.skill);
		if(skillLevel == 0)
			effort += 5;
		var difference = actionData.difficulty - skillLevel;
		effort += difference * 3;
		
		return Mathf.Max (1, effort);
	}

	static GameObject CreateStoryActionVisuals(StoryActionData data, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(storyActionPrefab) as GameObject;
		actionGO.GetComponent<StoryActionVisuals>().Setup(data.storyDescription, data.gameplayDescription);
		actionGO.GetComponent<StoryActionVisuals>().FinishedEvent += finishedAction;
		actionGO.GetComponent<StoryActionVisuals>().actionEvents = data.successEvents.ConvertAll(ae => ae.Create());

		return actionGO;
	}

	static StoryVisuals CreateStoryVisuals() {
		var go = GameObject.Instantiate(storyVisualsPrefab) as GameObject;
		var rt = go.GetComponent<RectTransform>();
		rt.SetParent(storyCanvas);
		rt.anchoredPosition = Vector2.zero;
		return go.GetComponent<StoryVisuals>();
	}

	public static EndPlayerTurnEvent CreateEndPlayerTurnEvent() {
		var e = new EndPlayerTurnEvent();
		e.controller = playerController;
		return e;
	}

	public static MovePlayerToPreviousPositionEvent CreateMovePlayerToPreviousPositionEvent() {
		var e = new MovePlayerToPreviousPositionEvent();
		e.playerController = playerController;
		return e;
	}

	public static GainSuppliesEvent CreateGainSuppliesEvent(int supplies) {
		var e = new GainSuppliesEvent();
		e.supplies = supplies;
		e.inventory = inventory;
		
		return e;
	}
}