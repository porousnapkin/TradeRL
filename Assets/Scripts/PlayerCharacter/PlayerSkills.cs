using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkills {
	public List<PlayerSkill> playerSkills = new List<PlayerSkill>();

    public PlayerSkills()
    {
        playerSkills = SkillsDatabase.Instance.allSkills.ConvertAll(s => {
            var playerSkill = DesertContext.StrangeNew<PlayerSkill>();
            playerSkill.skill = s;
            return playerSkill;
        });
    }

	public PlayerSkill GetSkill(SkillData s) {
		return playerSkills.Find(a => a.GetSkill() == s);
	}

	public int GetSkillLevel(SkillData s) {
		return GetSkill (s).GetLevel();
	}

    public void ReapplyAllSkills()
    {
        playerSkills.ForEach(a => a.ApplyAllLevels());
    }
}
