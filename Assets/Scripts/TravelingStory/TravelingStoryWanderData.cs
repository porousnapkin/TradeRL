public class TravelingStoryWanderData : TravelingStoryAIRoutineData {
	public float idleChance = 0.75f;

	public override TravelingStoryAIRoutine Create() {
		var ai = DesertContext.StrangeNew<TravelingStoryWander>();
		ai.idleChance = idleChance;
		return ai;
	}
}