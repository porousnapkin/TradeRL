public class TravelingStoryWanderData : TravelingStoryAIRoutineData {
	public int distanceToWander = 5;

	public override TravelingStoryAIRoutine Create() {
		var ai = DesertContext.StrangeNew<TravelingStoryWander>();
		ai.distanceToWander = distanceToWander;
        ai.speed = speed;
		return ai;
	}
}