public class RestEventData : StoryActionEventData
{
    public override StoryActionEvent Create()
    {
        return DesertContext.StrangeNew<RestEvent>();
    }
}