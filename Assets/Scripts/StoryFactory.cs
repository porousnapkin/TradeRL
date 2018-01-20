using UnityEngine;

public class StoryFactory {
	[Inject] public PlayerSkills playerSkills {private get; set; }
	[Inject] public GlobalTextArea textArea {private get; set;}
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

	public StoryVisuals CreateStory(StoryData sd, System.Action finishedAction) {
        GlobalEvents.StoryStarted();

		var visuals = CreateStoryVisuals();
		visuals.Setup (sd.title, sd.description);
        visuals.storyFinishedEvent += () =>
        {
            GlobalEvents.StoryFinished();
            finishedAction();
        };
		
		foreach(var sad in sd.actions)
			visuals.AddAction(CreateActionFromData(sad, () => visuals.Finished()));

		return visuals;
	}

	GameObject CreateActionFromData(StoryActionData sad, System.Action actionActivated) {
		if(sad.actionType == StoryActionData.ActionType.Skill)
			return CreateSkillStoryActionVisuals(CreateSkillStoryAction(sad), actionActivated);
		else
			return CreateStoryActionVisuals(sad, actionActivated);
	}
	
	GameObject CreateSkillStoryActionVisuals(SkillStoryAction a, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(PrefabGetter.skillStoryActionPrefab) as GameObject;
		actionGO.GetComponent<SkillStoryActionVisuals>().Setup(a);
		actionGO.GetComponent<SkillStoryActionVisuals>().ActivatedEvent += finishedAction;
		
		return actionGO;
	}
	
	SkillStoryAction CreateSkillStoryAction(StoryActionData actionData) {
		var sa = DesertContext.StrangeNew<SkillStoryAction>();

	    sa.skill = actionData.skill;
	    sa.difficulty = actionData.difficulty;
		sa.storyDescription = actionData.storyDescription;
		sa.gameDescription = actionData.gameplayDescription;
		sa.successMessage = actionData.successMessage;
		sa.failMessage = actionData.failedMessage;
		sa.successEvents = actionData.successEvents.ConvertAll(ae => ae.Create());
		sa.failEvents = actionData.failEvents.ConvertAll(ae => ae.Create());
	    sa.restrictions = actionData.restrictions.ConvertAll(r => r.Create(playerCharacter.GetCharacter()));
		
		return sa;
	}
	
	GameObject CreateStoryActionVisuals(StoryActionData data, System.Action actionActivated) {
		var actionGO = GameObject.Instantiate(PrefabGetter.storyActionPrefab) as GameObject;
        var visuals = actionGO.GetComponent<StoryActionVisuals>();
	    visuals.restrictions = data.restrictions.ConvertAll(r => r.Create(playerCharacter.GetCharacter()));
		visuals.Setup(data.storyDescription, data.gameplayDescription);
		visuals.ActivatedEvent += actionActivated;
		if(data.successMessage != "")
			visuals.FinishedEvent += () => textArea.AddLine(data.successMessage);
		visuals.actionEvents = data.successEvents.ConvertAll(ae => ae.Create());

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