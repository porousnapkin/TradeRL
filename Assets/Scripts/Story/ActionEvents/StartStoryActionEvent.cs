﻿using UnityEngine;
using System.Collections;
using System;

public class StartStoryActionEvent : StoryActionEvent
{
    [Inject] public StoryFactory storyFactory { private get; set; }
    public StoryData story;

    public void Activate()
    {
        storyFactory.CreateStory(story, () => { });
    }
}
