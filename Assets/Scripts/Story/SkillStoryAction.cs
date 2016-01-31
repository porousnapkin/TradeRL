using UnityEngine;
using System.Collections.Generic;

public class SkillStoryAction {
	[Inject] public Effort effort { private get; set; }

	public float chanceSuccess = 0.5f;
	public int effortToSurpass = 4;
	public string storyDescription = "Flee";
	public string gameDescription = "Attempt to escape the fight";

	public List<StoryActionEvent> successEvents;
	public List<StoryActionEvent> failEvents;

	public bool Attempt() {
		bool success = Random.value < chanceSuccess;
		if(success)
			foreach(var e in successEvents)
				e.Activate();
		else
			foreach(var e in failEvents)
				e.Activate();
		return success;
	}

	public bool CanAffordEffort() {
		return effort.Value >= effortToSurpass;
	}

	public void UseEffort() {
		effort.Spend(effortToSurpass);

		foreach(var e in successEvents)
			e.Activate();
	}
}