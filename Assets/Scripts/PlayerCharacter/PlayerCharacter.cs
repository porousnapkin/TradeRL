using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter {
	[Inject] public PlayerSkills skills { private get; set; }

	List<PlayerAbilityData> combatPlayerAbilities;
	List<PlayerAbilityModifierData> combatPlayerAbilityModifiers;
	Character playerCharacter;

	public Character GetCharacter()
	{
		return playerCharacter;
	}

	public void AddCombatPlayerAbility(PlayerAbilityData ability)
	{
		combatPlayerAbilities.Add(ability);
	}

	public void AddCombatPlayerAbilityModifier(PlayerAbilityModifierData modifier)
	{
		combatPlayerAbilityModifiers.Add(modifier);
	}
}
