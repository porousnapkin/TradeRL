using System;

public class SetActiveLocationOnCooldownEvent : StoryActionEvent
{
    [Inject] public Locations locations { private get; set; }
    public int daysCooledDown;

    public void Activate(Action callback)
    {
        locations.GetActiveLocation().SetupOnCooldown(daysCooledDown);
        callback();
    }
}

