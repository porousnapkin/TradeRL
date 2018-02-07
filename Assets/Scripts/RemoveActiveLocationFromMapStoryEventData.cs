public class RemoveActiveLocationFromMapStoryEventData : StoryActionEventData
{
    public override StoryActionEvent Create()
    {
        return DesertContext.StrangeNew<GainCitizenReputationAtTownStoryEvent>();
    }
}

