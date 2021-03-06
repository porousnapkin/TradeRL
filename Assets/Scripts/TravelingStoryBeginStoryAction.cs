public class TravelingStoryBeginStoryAction : TravelingStoryAction {
    [Inject] public StoryFactory storyFactory {private get; set;}
    public StoryData story {private get; set;}

    public void Activate(System.Action finishedDelegate, bool playerInitiated) {
        storyFactory.CreateStory(story, finishedDelegate);
    }
}