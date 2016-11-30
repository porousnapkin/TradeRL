using UnityEngine;
using System.Collections;

public class GainMapAbilitySkillLevelBenefit : SkillLevelBenefit 
{
	public MapAbilityData mapAbility;

	public override void Apply (PlayerCharacter playerCharacter)
	{
		var gainer = DesertContext.StrangeNew<MapAbilityGainer>();
		gainer.mapAbility = mapAbility.Create(playerCharacter.GetCharacter());
		gainer.Apply();
	}
}

public class MapAbilityGainer
{
	[Inject]public MapAbilityButtons mapAbilityButtons {private get; set;}
	public MapPlayerAbility mapAbility {private get; set;}

	public void Apply() 
	{
		mapAbilityButtons.AddButton(mapAbility);
	}
}
