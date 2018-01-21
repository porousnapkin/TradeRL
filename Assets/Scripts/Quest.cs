using System.Collections.Generic;

public class Quest
{
    [Inject]
    public GlobalTextArea textArea { private get; set; }
	public List<StoryActionEvent> successEvents = new List<StoryActionEvent>();
    public string id;
    public string title;
    public string description;

    public void Begin()
    {
        textArea.AddLine("Began " + title + " quest.");
    }

    public void ApplyQuestCompletionAffects(System.Action callback)
    {
        PerformNextEvent(callback);
        textArea.AddLine("Finished " + title + " quest.");
    }

    void PerformNextEvent(System.Action callback, int eventIndex = 0)
    {
        if(eventIndex >= successEvents.Count)
        {
            callback();
            return;
        }

        successEvents[eventIndex].Activate(() => PerformNextEvent(callback, eventIndex + 1));
    }
}

