using UnityEngine;
using System.Collections;
using System;

public class CreateMapLocationEventData : StoryActionEventData
{
    public LocationData location;

    public override StoryActionEvent Create()
    {
        var action = DesertContext.StrangeNew<CreateMapLocationEvent>();
        action.locationData = location;
        return action;
    }
}
