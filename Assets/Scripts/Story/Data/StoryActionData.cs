using UnityEngine;
using System.Collections.Generic;

//Different or same type for skill vs. actualized?
public class StoryActionData : ScriptableObject {
	public enum ActionType {
		Skill,
		Immediate,
	}
	public ActionType actionType;

	public string skillType = "";	
	public string shortDescription = "Flee";
	public string longDescription = "Attempt to escape the fight";
	public List<StoryActionEventData> successEvents = new List<StoryActionEventData>();
	public List<StoryActionEventData> failEvents = new List<StoryActionEventData>();

	public GameObject Create(System.Action finishedAction) {
		if(actionType == ActionType.Skill) {
			var sa = StoryFactory.CreateSkillStoryAction();

			//Debug till there's a skill system
			sa.chanceSuccess = 0.5f;
			sa.effortToSurpass = 4;

			sa.shortDescription = shortDescription;
			sa.longDescription = longDescription;	
			sa.successEvents = successEvents.ConvertAll(e => e.Create());
			sa.failEvents = failEvents.ConvertAll(e => e.Create());

			return StoryFactory.CreateSkillStoryActionVisuals(sa, finishedAction);
		}
		else {
			return StoryFactory.CreateStoryActionVisuals(this, finishedAction);
		}
	}
}