public class TravelingStoryGuardData : TravelingStoryAIRoutineData
{
    public override TravelingStoryAIRoutine Create()
    {
        var ai = DesertContext.StrangeNew<TravelingStoryGuard>();
        return ai;
    }
}