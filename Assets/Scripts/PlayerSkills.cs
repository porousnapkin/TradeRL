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
		var skill = playerSkills.Find(a => a.GetSkill() == s);
        if(skill == null)
            UnityEngine.Debug.LogError("Skill " + s.name + " has not been added to the skill database");
        return skill;
	}

	public int GetSkillLevel(SkillData s) {
		return GetSkill (s).GetLevel();
	}

    public void ReapplyAllSkills()
    {
        playerSkills.ForEach(a => a.ApplyAllLevels());
    }
}
