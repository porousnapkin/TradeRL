public class TravelingStoryFleeData : TravelingStoryAIRoutineData {
	public override TravelingStoryAIRoutine Create() {
		return DesertContext.StrangeNew<TravelingStoryFlee>();
	}
}