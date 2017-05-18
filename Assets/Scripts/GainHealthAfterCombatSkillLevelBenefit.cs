public class GainHealthAfterCombatSkillLevelBenefit : SkillLevelBenefit 
{
	public int amount = 2;
    public string source = "Survival";

	public override void Apply (PlayerCharacter playerCharacter)
	{
		var gainer = DesertContext.StrangeNew<GainHealthAfterCombat>();
        gainer.amount = amount;
        gainer.source = source;
		gainer.Apply(playerCharacter.GetCharacter().health, amount);
	}
}
