using UnityEngine;
using System.Collections;

public class PlayerSkill {
	[Inject] public PlayerCharacter playerCharacter { private get; set; }
	public SkillData skill { private get; set;}
	public SkillData GetSkill() { return skill; }
	int level = 0;
	public int GetLevel() { return level; }
    public void SetLevel(int level) { this.level = level; }

	public void LevelUp() {
		level++;
		skill.HandleLevelUp(playerCharacter, level);
	}

    public void ApplyAllLevels()
    {
        for (int i = 1; i <= level; i++)
            skill.HandleLevelUp(playerCharacter, i);
    }
}
