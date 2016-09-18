public class AdvanceDaysEvent : StoryActionEvent
{
    [Inject] public GameDate gameDate { private get; set; }
    public int daysToAdvance { private get; set; }

    public void Activate()
    {
        gameDate.AdvanceDays(daysToAdvance);
    }
}