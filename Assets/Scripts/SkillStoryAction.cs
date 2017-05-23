using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillStoryAction {
	[Inject] public Effort effort { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set; }
    [Inject] public PlayerSkills playerSkills { private get; set; }

    public SkillData skill;
    public int difficulty;
	public string storyDescription = "Flee";
	public string gameDescription = "Attempt to escape the fight";
	public string successMessage = "";
	public string failMessage = "";

	public List<StoryActionEvent> successEvents;
	public List<StoryActionEvent> failEvents;
    public List<Restriction> restrictions;

	public bool Attempt() {
		bool success = UnityEngine.Random.value < CalculateChanceOfSuccess();
		if(success)
			Succeed();
		else
			Fail();
		return success;
	}

	void Succeed() {
		if(successMessage != "")
			textArea.AddLine(successMessage);
		foreach(var e in successEvents)
			e.Activate();
	}

	void Fail() {
		if(failMessage != "")
			textArea.AddLine(failMessage);
		foreach(var e in failEvents)
			e.Activate();
	}

    public bool CanUse()
    {
        return restrictions.TrueForAll(r => r.CanUse());
    }

    public bool CanAffordEffort()
    {
        return effort.GetEffort(skill.effortType) >= CalculateEffort();
	}

	public void UseEffort() {
        effort.SafeSubtractEffort(skill.effortType, CalculateEffort());

		foreach(var e in successEvents)
			e.Activate();
	}

    public float CalculateChanceOfSuccess() {
        var effortCost = CalculateEffort();
        if (effortCost <= 0)
            return 1.0f;
        return Mathf.Max(0.9f - (0.1f * effortCost), 0.05f);
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