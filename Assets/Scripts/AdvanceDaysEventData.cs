public class AdvanceDaysEventData : StoryActionEventData
{
    public int daysToPass = 1;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<AdvanceDaysEvent>();
        e.daysToAdvance = daysToPass;
        return e;
    }
}