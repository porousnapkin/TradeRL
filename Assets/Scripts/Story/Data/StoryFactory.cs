using UnityEngine;

public class StoryFactory {
	public static Effort effort;
	public static GameObject storyVisualsPrefab;
	public static Transform storyCanvas;
	public static GameObject skillStoryActionPrefab;
	public static GameObject storyActionPrefab;
	public static PlayerController playerController; //TODO

	public static SkillStoryAction CreateSkillStoryAction() {
		var sa = new SkillStoryAction();
		sa.effort = effort;
		return sa;
	}	

	public static GameObject CreateSkillStoryActionVisuals(SkillStoryAction a, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(skillStoryActionPrefab) as GameObject;
		actionGO.GetComponent<SkillStoryActionVisuals>().Setup(a);
		actionGO.GetComponent<SkillStoryActionVisuals>().FinishedEvent += finishedAction;

		return actionGO;
	}

	public static GameObject CreateStoryActionVisuals(StoryActionData data, System.Action finishedAction) {
		var actionGO = GameObject.Instantiate(storyActionPrefab) as GameObject;
		actionGO.GetComponent<StoryActionVisuals>().Setup(data.shortDescription, data.longDescription);
		actionGO.GetComponent<StoryActionVisuals>().FinishedEvent += finishedAction;
		actionGO.GetComponent<StoryActionVisuals>().actionEvents = data.successEvents.ConvertAll(ae => ae.Create());

		return actionGO;
	}

	public static StoryVisuals CreateStoryVisuals() {
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
}