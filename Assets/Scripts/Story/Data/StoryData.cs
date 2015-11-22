using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StoryData : ScriptableObject {
	public string description;
	public List<StoryActionData> actions;

	public StoryVisuals Create(System.Action finished) {
		return StoryFactory.CreateStory(this, finished);
	}
}