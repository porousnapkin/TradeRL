public class AllyStabilizationEvent : StoryActionEvent
{
    [Inject]
    public PlayerTeam playerTeam { private get; set; }
    public bool stabilizes = true;

    public void Activate(System.Action callback)
    {
        var teammate = playerTeam.GetATeammateReadyToStabilize();
        if (stabilizes)
            playerTeam.TeammateStabilized(teammate);
        else
            playerTeam.TeammateFailedToStabilize(teammate);

        callback();
    }
}

