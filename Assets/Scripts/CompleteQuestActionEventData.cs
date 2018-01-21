public class CompleteQuestActionEventData : StoryActionEventData
{
    public QuestData questData;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<CompleteQuestActionEvent>();
        e.quest = questData;
        return e;
    }
}

