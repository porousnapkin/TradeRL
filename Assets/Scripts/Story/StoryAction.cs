using UnityEngine;

public class StoryAction {
	public float chanceSuccess = 0.5f;
	public int effortToSurpass = 4;
	public string shortDescription = "Flee";
	public string longDescription = "Attempt to escape the fight";
	public Effort effort;

	public bool Attempt() {
		return Random.value < chanceSuccess;
	}

	public bool CanAffordEffort() {
		return effort.Value >= effortToSurpass;
	}

	public void UseEffort() {
		effort.Spend(effortToSurpass);
	}
}