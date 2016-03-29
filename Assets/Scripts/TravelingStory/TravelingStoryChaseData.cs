public class TravelingStoryChaseData : TravelingStoryAIRoutineData {
	public override TravelingStoryAIRoutine Create() {
		var chase = DesertContext.StrangeNew<TravelingStoryChase>();
        chase.speed = this.speed;
        return chase;
	}
}