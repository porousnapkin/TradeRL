using UnityEngine;
using System.Collections.Generic;

//Different or same type for skill vs. actualized?
public class StoryActionData : ScriptableObject {
	public enum ActionType {
		Skill,
		Immediate,
	}
	public ActionType actionType;

	public Skill skill;	
	public int difficulty = 2;
	public string storyDescription = "Story Description";
	public string gameplayDescription = "Gameplay Description";
	public List<StoryActionEventData> successEvents = new List<StoryActionEventData>();
	public List<StoryActionEventData> failEvents = new List<StoryActionEventData>();
}