public class CompleteQuestActionEvent : StoryActionEvent
{
    [Inject] public Quests quests { private get; set; }
    public QuestData quest;

    public void Activate(System.Action callback)
    {
        quests.FinishQuest(quest);
        callback();
    }
}

