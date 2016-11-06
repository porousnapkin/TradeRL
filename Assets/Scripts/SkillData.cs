using UnityEngine;
using System.Collections.Generic;

public class SkillData : ScriptableObject {
	public string displayName = "SkillName";
	public string description = "This skill is cool";
    public Effort.EffortType effortType;
    public List<List<SkillLevelBenefit>> levelBenefits = new List<List<SkillLevelBenefit>>();

	public void HandleLevelUp(PlayerCharacter player, int newLevel) 
	{
		var benefits = levelBenefits[newLevel - 1];
		if(benefits != null)
			benefits.ForEach(b => b.Apply(player));
	}
}
