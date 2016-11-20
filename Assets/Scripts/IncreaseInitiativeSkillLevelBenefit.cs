public class IncreaseInitiativeSkillLevelBenefit : SkillLevelBenefit
{
    public int amount;

    public override void Apply(PlayerCharacter playerCharacter)
    {
        playerCharacter.GetCharacter().speed += amount;
    }
}


