using System;

class MapAbilityStartStoryActivator : MapAbilityActivator
{
    [Inject] public StoryFactory storyFactory { private get; set; }
    public StoryData story;

    public void Activate(Action callback)
    {
        storyFactory.CreateStory(story, callback);
    }
}