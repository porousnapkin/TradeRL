using System;

public class GainCombatAbilityModifierSkillLevelBenefit : SkillLevelBenefit
{
	public PlayerAbilityModifierData ability;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		playerCharacter.AddCombatPlayerAbilityModifier(ability);
	}
}

