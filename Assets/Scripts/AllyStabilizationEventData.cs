public class AllyStabilizationEventData : StoryActionEventData
{
    public bool stabilizes = true;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<AllyStabilizationEvent>();
        e.stabilizes = stabilizes;
        return e;
    }
}

