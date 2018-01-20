public class GainCitizenReputationAtTownStoryEventData : StoryActionEventData
{
    public TownData townData;
    public enum xpAmounts
    {
        small = 20,
        medium = 50,
        large = 100
    }
    public xpAmounts xpAmount;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<GainCitizenReputationAtTownStoryEvent>();
        e.townData = townData;
        e.xpAmount = (int)xpAmount;
        return e;
    }
}

