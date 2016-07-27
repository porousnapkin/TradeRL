using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkills {
	public List<PlayerSkill> playerSkills = new List<PlayerSkill>();

	public PlayerSkills() {
	}

	public PlayerSkill GetSkill(SkillData s) {
		return playerSkills.Find(a => a.GetSkill() == s);
	}

	public int GetSkillLevel(SkillData s) {
		return GetSkill (s).GetLevel();
	}
}
