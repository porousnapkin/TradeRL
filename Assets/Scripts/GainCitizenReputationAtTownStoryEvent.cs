using System;

public class GainCitizenReputationAtTownStoryEvent : StoryActionEvent
{
    [Inject] public Towns towns { private get; set; }
    public TownData townData;
    public int xpAmount = 20;

    public void Activate(Action callback)
    {
        towns.GetTown(townData.name).citizensReputation.GainXP(xpAmount);
        callback();
    }
}

