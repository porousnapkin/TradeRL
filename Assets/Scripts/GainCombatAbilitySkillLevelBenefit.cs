public class GainCombatAbilitySkillLevelBenefit : SkillLevelBenefit
{
	public PlayerAbilityData ability;
	
	public override void Apply (PlayerCharacter playerCharacter)
	{
		playerCharacter.AddCombatPlayerAbility(ability);
	}
}

