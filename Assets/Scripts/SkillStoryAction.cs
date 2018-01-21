using UnityEngine;
using System.Collections.Generic;

public class SkillStoryAction {
	[Inject] public Effort effort { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set; }
    [Inject] public PlayerSkills playerSkills { private get; set; }

    public SkillData skill;
    public int difficulty;
	public string storyDescription = "Flee";
	public string gameDescription = "Escape the fight";
	public string successMessage = "";

	public List<StoryActionEvent> successEvents;
    public List<Restriction> restrictions;
    private int actionIndex;

    void ActivateEvents(List<StoryActionEvent> events, System.Action callback)
    {
        actionIndex++;
        if (actionIndex >= events.Count)
        {
            callback();
            return;
        }

        events[actionIndex].Activate(() => ActivateEvents(events, callback));
    }

    public bool CanUse()
    {
        return restrictions.TrueForAll(r => r.CanUse()) && CanAffordEffort();
    }

    bool CanAffordEffort()
    {
        return effort.GetEffort(skill.effortType) >= CalculateEffort();
	}

	public void SucceedUsingEffort(System.Action callback) {
        effort.SafeSubtractEffort(skill.effortType, CalculateEffort());
        if (successMessage != "")
            textArea.AddLine(successMessage);

        actionIndex = -1;
        ActivateEvents(successEvents, callback);
	}

    public int CalculateEffort() {
		int effort = 1;
		var skillLevel = playerSkills.GetSkillLevel(skill);
		var difference = difficulty - skillLevel;

        for (int i = 0; i < difference; i++)
            effort += (i + 1);
		
		return Mathf.Max (1, effort);
	}

    public Effort.EffortType GetEffortType()
    {
        return skill.effortType;
    }
}