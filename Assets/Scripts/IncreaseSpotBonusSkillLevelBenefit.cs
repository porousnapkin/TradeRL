using UnityEngine;
using System.Collections;

public class IncreaseSpotBonusSkillLevelBenefit : SkillLevelBenefit 
{
	public int amount = 2;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		playerCharacter.SetSpotBonus(playerCharacter.GetSpotBonus() + amount);
	}
}
