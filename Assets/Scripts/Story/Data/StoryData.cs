using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StoryData : ScriptableObject {
	public string description;
	public List<StoryActionData> actions;

	public StoryVisuals Create() {
		var sv = StoryFactory.CreateStoryVisuals();	

		sv.Setup(description, actions.ConvertAll(a => a.Create(sv.Finished)));
		return sv;
	}
}