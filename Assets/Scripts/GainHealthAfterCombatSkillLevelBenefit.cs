﻿public class GainHealthAfterCombatSkillLevelBenefit : SkillLevelBenefit 
{
	public int amount = 2;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		var gainer = new GainHealthAfterCombat();
		gainer.Apply(playerCharacter.GetCharacter().health, amount);
	}
}