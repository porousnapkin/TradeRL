using System.Collections.Generic;
using UnityEngine;

public class Quests
{
    Dictionary<string, Quest> activeQuestsById = new Dictionary<string, Quest>();

    public void AddActiveQuest(Quest quest)
    {
        quest.Begin();
        activeQuestsById[quest.id] = (quest);
    }

    public void FinishQuest(QuestData questData)
    {
        var q = GetActiveQuest(questData);
        q.ApplyQuestCompletionAffects(() => activeQuestsById.Remove(q.id));
    }

    Quest GetActiveQuest(QuestData questData)
    {
        if(activeQuestsById.ContainsKey(questData.name))
            return activeQuestsById[questData.name];

        Debug.LogError("Attempted to get quest " + questData.name + " when that quest was not an active quest");
        return null;
    }
}

