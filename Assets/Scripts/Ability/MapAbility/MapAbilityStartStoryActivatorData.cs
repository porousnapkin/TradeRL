using UnityEngine;
using System.Collections;
using System;

public class MapAbilityStartStoryActivatorData : MapAbilityActivatorData
{
    public StoryData story;

    public override MapAbilityActivator Create()
    {
        var activator = DesertContext.StrangeNew<MapAbilityStartStoryActivator>();
        activator.story = story;
        return activator;
    }
}
