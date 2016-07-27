using UnityEngine;
using System.Collections;

public class PlayerSkill {
	[Inject] public PlayerCharacter playerCharacter { private get; set; }
	public SkillData skill { private get; set;}
	public SkillData GetSkill() { return skill; }
	int level = 0;
	public int GetLevel() { return level; }

	public PlayerSkill(SkillData s) {
		skill = s;
	}

	public void LevelUp() {
		level++;
		skill.HandleLevelUp(playerCharacter, level);
	}
}
