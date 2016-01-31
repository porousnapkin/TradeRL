using UnityEngine;

public class StoryFactory {
	[Inject] public PlayerSkills playerSkills {private get; set; }

	public StoryVisuals CreateStory(StoryData sd, System.Action finishedAction) {
		var visuals = CreateStoryVisuals();
		visuals.Setup (sd.title, sd.description);
		visuals.storyFinishedEvent += finishedAction;
		
		foreach(var sad in sd.actions)
			visuals.AddAction(CreateSkillStoryActionVisualsFromData(sad, () => visuals.Finished()));

		return visuals;
	}

	GameObject CreateSkillStoryActionVisualsFromData(StoryActionData sad, System.Action finishedAction) {
		if(sad.actionType == StoryActionData.ActionType.Skill)
			return CreateSkillStoryActionVisuals(CreateSkillStoryAction(sad), finishedAction);
		else
			return CreateStoryActionVisuals(sad, finishedAction);
	}
	
	GameObject CreateSkillStoryActionVisuals(SkillStoryAction a, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(PrefabGetter.skillStoryActionPrefab) as GameObject;
		actionGO.GetComponent<SkillStoryActionVisuals>().Setup(a);
		actionGO.GetComponent<SkillStoryActionVisuals>().FinishedEvent += finishedAction;
		
		return actionGO;
	}
	
	SkillStoryAction CreateSkillStoryAction(StoryActionData actionData) {
		var sa = DesertContext.StrangeNew<SkillStoryAction>();
		
		sa.chanceSuccess = CalculateChanceOfSuccess(actionData);
		sa.effortToSurpass = CalculateEffort(actionData);
		sa.storyDescription = actionData.storyDescription;
		sa.gameDescription = actionData.gameplayDescription;
		sa.successEvents = actionData.successEvents.ConvertAll(ae => ae.Create());
		sa.failEvents = actionData.failEvents.ConvertAll(ae => ae.Create());
		
		return sa;
	}
	
	float CalculateChanceOfSuccess(StoryActionData actionData) {
		float chanceOffset = 0.0f;
		var skillLevel = playerSkills.GetSkillLevel(actionData.skill);
		if(skillLevel == 0)
			chanceOffset = 0.4f;
		var difference = actionData.difficulty - skillLevel;
		chanceOffset += 0.2f * difference;
		
		return Mathf.Max (0.1f, 0.9f - chanceOffset);
	}
	
	int CalculateEffort(StoryActionData actionData) {
		int effort = 0;
		var skillLevel = playerSkills.GetSkillLevel(actionData.skill);
		if(skillLevel == 0)
			effort += 5;
		var difference = actionData.difficulty - skillLevel;
		effort += difference * 3;
		
		return Mathf.Max (1, effort);
	}

	GameObject CreateStoryActionVisuals(StoryActionData data, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(PrefabGetter.storyActionPrefab) as GameObject;
		actionGO.GetComponent<StoryActionVisuals>().Setup(data.storyDescription, data.gameplayDescription);
		actionGO.GetComponent<StoryActionVisuals>().FinishedEvent += finishedAction;
		actionGO.GetComponent<StoryActionVisuals>().actionEvents = data.successEvents.ConvertAll(ae => ae.Create());

		return actionGO;
	}

	StoryVisuals CreateStoryVisuals() {
		var go = GameObject.Instantiate(PrefabGetter.storyVisualsPrefab) as GameObject;
		var rt = go.GetComponent<RectTransform>();
		rt.SetParent(PrefabGetter.baseCanvas);
		rt.anchoredPosition = Vector2.zero;
		return go.GetComponent<StoryVisuals>();
	}
}