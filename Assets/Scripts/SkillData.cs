using UnityEngine;
using System.Collections.Generic;

public class SkillData : ScriptableObject {
	public string displayName = "SkillName";
	public string description = "This skill is cool";
    public Effort.EffortType effortType;
	public List<SkillLevelBenefit> level1Benefits = new List<SkillLevelBenefit>();
	public List<SkillLevelBenefit> level2Benefits = new List<SkillLevelBenefit>();
	public List<SkillLevelBenefit> level3Benefits = new List<SkillLevelBenefit>();
	public List<SkillLevelBenefit> level4Benefits = new List<SkillLevelBenefit>();
	public List<SkillLevelBenefit> level5Benefits = new List<SkillLevelBenefit>();

	public void HandleLevelUp(PlayerCharacter player, int newLevel) 
	{
		var benefits = GetBenefitsForLevel(newLevel);
		if(benefits != null)
			benefits.ForEach(b => b.Apply(player));
	}

	List<SkillLevelBenefit> GetBenefitsForLevel(int level)
	{
		switch(level)
		{
		case 1: return level1Benefits;
		case 2: return level2Benefits;
		case 3: return level3Benefits;
		case 4: return level4Benefits;
		case 5: return level5Benefits;
		default: return null;
		}
	}
}
