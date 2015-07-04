using UnityEngine;

//Different or same type for skill vs. actualized?
public class StoryActionData : ScriptableObject {
	public string skillType = "";	
	public string shortDescription = "Flee";
	public string longDescription = "Attempt to escape the fight";

	public StoryAction Create() {
		var sa = StoryFactory.CreateStoryAction();

		//Debug till there's a skill system
		sa.chanceSuccess = 0.5f;
		sa.effortToSurpass = 4;

		sa.shortDescription = shortDescription;
		sa.longDescription = longDescription;	

		return sa;
	}
}