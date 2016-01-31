using UnityEngine;
using System.Collections;

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

	public void Create(Vector2 position) {
		var travelingStory = DesertContext.StrangeNew<TravelingStory>();
		travelingStory.data = this;
		travelingStory.WorldPosition = position;

		DesertContext.QuickBind(travelingStory);
		GameObject.Instantiate(PrefabGetter.travelingStoryPrefab);
		DesertContext.FinishQuickBind<TravelingStory>();
	}
}
