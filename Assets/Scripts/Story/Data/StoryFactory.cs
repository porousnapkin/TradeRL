using UnityEngine;

public class StoryFactory {
	public static Effort effort;
	public static GameObject storyVisualsPrefab;
	public static Transform storyCanvas;

	public static StoryAction CreateStoryAction() {
		var sa = new StoryAction();
		sa.effort = effort;
		return sa;
	}	

	public static StoryVisuals CreateStoryVisuals() {
		var go = GameObject.Instantiate(storyVisualsPrefab) as GameObject;
		var rt = go.GetComponent<RectTransform>();
		rt.SetParent(storyCanvas);
		rt.anchoredPosition = Vector2.zero;
		return go.GetComponent<StoryVisuals>();
	}
}