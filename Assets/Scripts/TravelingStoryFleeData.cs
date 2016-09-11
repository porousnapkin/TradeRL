public class TravelingStoryFleeData : TravelingStoryAIRoutineData {
	public override TravelingStoryAIRoutine Create() {
		var flee = DesertContext.StrangeNew<TravelingStoryFlee>();
        flee.speed = this.speed;
        return flee;
	}
}