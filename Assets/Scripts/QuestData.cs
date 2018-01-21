using System.Collections.Generic;
using UnityEngine;

public class QuestData : ScriptableObject
{
	public List<StoryActionEventData> successEvents = new List<StoryActionEventData>();
    public string title;
    public string description;

    public Quest Create()
    {
        var q = DesertContext.StrangeNew<Quest>();
        q.id = name;
        q.title = title;
        q.description = description;
        q.successEvents = successEvents.ConvertAll(d => d.Create());
        return q;
    }
}

