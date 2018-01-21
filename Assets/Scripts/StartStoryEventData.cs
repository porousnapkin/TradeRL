public class StartStoryEventData : StoryActionEventData
{
    public StoryData storyData;

    public override StoryActionEvent Create()
    {
        var action = DesertContext.StrangeNew<StartStoryActionEvent>();
        action.story = storyData;
        return action;
    }
}
