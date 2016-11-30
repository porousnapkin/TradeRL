using UnityEngine;
using System.Collections;

public class IncreaseJamSavesSkillLevelBenefit : SkillLevelBenefit
{
	public int jamSavesGained = 1;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		var increaser = DesertContext.StrangeNew<JamSavesIncreaser>();
		increaser.increaseAmount = jamSavesGained;
		increaser.Run();
	}
}

public class JamSavesIncreaser
{
	[Inject] public Inventory inventory {private get; set; }
	public int increaseAmount;

	public void Run() 
	{
		inventory.SetBaseJamSaves(inventory.GetBaseJamSaves() + increaseAmount);
	}
}
