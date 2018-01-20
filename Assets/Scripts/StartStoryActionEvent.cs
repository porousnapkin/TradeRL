public class StartStoryActionEvent : StoryActionEvent
{
    [Inject] public StoryFactory storyFactory { private get; set; }
    public StoryData story;

    public void Activate(System.Action callback)
    {
        storyFactory.CreateStory(story, callback);
    }
}
