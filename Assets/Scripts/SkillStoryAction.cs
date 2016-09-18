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
		float chanceOffset = 0.0f;
		var skillLevel = playerSkills.GetSkillLevel(skill);
		if(skillLevel == 0)
			chanceOffset = 0.4f;
		var difference = difficulty - skillLevel;
		chanceOffset += 0.2f * difference;
		
		return Mathf.Max (0.1f, 0.9f - chanceOffset);
	}

    public int CalculateEffort() {
		int effort = 0;
		var skillLevel = playerSkills.GetSkillLevel(skill);
		if(skillLevel == 0)
			effort += 2;
		var difference = difficulty - skillLevel;
		effort += difference;
		
		return Mathf.Max (0, effort);
	}

    public Effort.EffortType GetEffortType()
    {
        return skill.effortType;
    }
}