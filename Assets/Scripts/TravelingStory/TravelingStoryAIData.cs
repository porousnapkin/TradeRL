using UnityEngine;

public class TravelingStoryAIData : ScriptableObject {
	public TravelingStoryAIRoutineData startingRoutine;
	public int closeDistance = 4;
	public TravelingStoryAIRoutineData closeAI;
	public int farDistance = 10;
	public TravelingStoryAIRoutineData farAI;

	public TravelingStoryAI Create() {
		var ai = DesertContext.StrangeNew<TravelingStoryAI>();
		ai.activeRoutine = startingRoutine.Create();
		ai.closeTriggerDistance = closeDistance;
		ai.closeAI = closeAI.Create();
		ai.farTriggerDistance = farDistance;
		ai.farAI = farAI.Create();

		return ai;
	}
}
