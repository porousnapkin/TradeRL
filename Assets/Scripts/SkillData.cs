using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ListOfSkillBenefits
{
    public List<SkillLevelBenefit> listOfBenefits = new List<SkillLevelBenefit>();
}

public class SkillData : ScriptableObject {
	public string displayName = "SkillName";
	public string description = "This skill is cool";
    public Effort.EffortType effortType;
    public List<ListOfSkillBenefits> levelBenefits = new List<ListOfSkillBenefits>();

	public void HandleLevelUp(PlayerCharacter player, int newLevel) 
	{
		var benefits = levelBenefits[newLevel - 1];
		if(benefits != null)
			benefits.listOfBenefits.ForEach(b => b.Apply(player));
	}
}
