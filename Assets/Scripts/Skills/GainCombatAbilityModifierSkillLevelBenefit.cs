using System;

public class GainCombatAbilityModifierSkillLevelBenefit : SkillLevelBenefit
{
	public PlayerAbilityModifierData ability;

	public override void Apply (PlayerCharacter playerCharacter)
	{
        UnityEngine.Debug.Log("Applying this.");
		playerCharacter.AddCombatPlayerAbilityModifier(ability);
	}
}

