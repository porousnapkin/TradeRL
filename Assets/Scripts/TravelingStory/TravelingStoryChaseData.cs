public class TravelingStoryChaseData : TravelingStoryAIRoutineData {
	public override TravelingStoryAIRoutine Create() {
		return DesertContext.StrangeNew<TravelingStoryChase>();
	}
}