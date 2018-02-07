using System;

public class RemoveActiveLocationFromMapStoryEvent : StoryActionEvent
{
    [Inject] public Locations locations { private get; set; }

    public void Activate(Action callback)
    {
        locations.GetActiveLocation().Remove();
        callback();
    }
}

