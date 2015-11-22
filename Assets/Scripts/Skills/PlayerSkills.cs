using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSkills {
	public List<PlayerSkill> playerSkills = new List<PlayerSkill>();

	public PlayerSkills() {
		var skills = Resources.LoadAll<Skill>("Skills");
		foreach(var s in skills)
			playerSkills.Add (new PlayerSkill(s));
	}

	public PlayerSkill GetSkill(Skill s) {
		return playerSkills.Find(a => a.Skill == s);
	}

	public int GetSkillLevel(Skill s) {
		return GetSkill (s).Level;
	}
}
