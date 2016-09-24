public class GainAmbushAbilitySkillLevelBenefit : SkillLevelBenefit
{
    public PlayerAbilityData ability;
	
    public override void Apply (PlayerCharacter playerCharacter)
    {
        playerCharacter.AddAmbushAbility(ability);
    }
}
