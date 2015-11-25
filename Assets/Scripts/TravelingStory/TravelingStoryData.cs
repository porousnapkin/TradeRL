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

	public void Activate(System.Action finishedDelegate) {
		switch(stepInAction) {
		case StepInAction.BeginStory:
			story.Create(finishedDelegate); break;
		case StepInAction.Combat:
			combatData.Create(); break;
		}
	}

	public void Create(Vector2 pos) {
		TravelingStoryFactory.CreateTravelingStory(this, pos);
	}
}
