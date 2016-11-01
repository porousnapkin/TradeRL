using System;

public class IncreaseMaxHPSkillLevelBenefit : SkillLevelBenefit
{
	public int amount;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		playerCharacter.GetCharacter().health.MaxValue += amount;
		playerCharacter.GetCharacter().health.Value += amount;
	}
}

