public class SetActiveLocationOnCooldownEventData : StoryActionEventData
{
    public enum CooldownAmount
    {
        Short = 30,
        Medium = 100,
        Long = 350,
    }
    public CooldownAmount cooldownAmount;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<SetActiveLocationOnCooldownEvent>();
        e.daysCooledDown = (int)cooldownAmount;
        return e;
    }
}

