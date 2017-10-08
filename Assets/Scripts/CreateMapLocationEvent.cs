using UnityEngine;
using System.Collections;
using System;

public class CreateMapLocationEvent : StoryActionEvent
{
    [Inject] public LocationFactory locationFactory { private get; set; }
    [Inject] public MapPlayerController mapPlayerController { private get; set; }
    public LocationData locationData { private get; set; }

    public void Activate(System.Action callback)
    {
        locationFactory.AddLocationToPosition(locationData, (int)mapPlayerController.position.x, (int)mapPlayerController.position.y);
        callback();
    }
}
