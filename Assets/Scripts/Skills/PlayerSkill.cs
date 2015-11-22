using UnityEngine;
using System.Collections;

public class PlayerSkill {
	Skill skill;
	public Skill Skill { get { return skill; }}
	int level = 0;
	public int Level { get { return level; }}

	public PlayerSkill(Skill s) {
		skill = s;
	}

	public void LevelUp() {
		level++;
		skill.HandleLevelUp(level);
	}
}
