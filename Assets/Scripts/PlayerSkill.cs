public class PlayerSkill {
	[Inject] public PlayerCharacter playerCharacter { private get; set; }
    [Inject] public Effort effort { private get; set; }
	public SkillData skill { private get; set;}
	public SkillData GetSkill() { return skill; }
	int level = 0;
	public int GetLevel() { return level; }
    public void SetLevel(int level) {
        int difference = level - this.level;
        for (int i = 0; i < difference; i++)
            LevelUp();
    }

	public void LevelUp() {
		level++;
		skill.HandleLevelUp(playerCharacter, level);

        var maxEffort = effort.GetMaxEffort(skill.effortType);
        effort.SetMaxEffort(skill.effortType, maxEffort + 1);
        var curEffort = effort.GetEffort(skill.effortType);
        effort.SetEffort(skill.effortType, curEffort + 1);
    }
}
