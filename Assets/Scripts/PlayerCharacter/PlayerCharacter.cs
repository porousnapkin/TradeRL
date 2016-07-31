using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter {
	[Inject] public PlayerSkills skills { private get; set; }

	List<PlayerAbilityData> combatPlayerAbilities = new List<PlayerAbilityData>();
	List<PlayerAbilityModifierData> combatPlayerAbilityModifiers = new List<PlayerAbilityModifierData>();
	Character playerCharacter;

    public void BuildCharacter()
    {
        playerCharacter = DesertContext.StrangeNew<Character>();

        var baseStats = BasePlayerCharacterStats.Instance;
        playerCharacter.Setup(baseStats.maxHP);
        playerCharacter.attackModule = CreateAttackModule(baseStats);
        playerCharacter.defenseModule = CreateDefenseModule(baseStats);
        playerCharacter.speed = baseStats.baseInitiative;
        playerCharacter.myFaction = Faction.Player;
        playerCharacter.displayName = "<color=Orange>" + "PLAYA" + "</color>";

        baseStats.defaultAbilities.ForEach(a => AddCombatPlayerAbility(a));
        baseStats.defaultAbilityModifiers.ForEach(m => AddCombatPlayerAbilityModifier(m));

        skills.ReapplyAllSkills();
    }

    AttackModule CreateAttackModule(BasePlayerCharacterStats baseStats)
    {
        var attackModule = new AttackModule();
        attackModule.minDamage = baseStats.baseMeleeDamage;
        attackModule.maxDamage = baseStats.baseMeleeDamage;

        return attackModule;
    }

    DefenseModule CreateDefenseModule(BasePlayerCharacterStats baseStats)
    {
        var defenseModule = new DefenseModule();
        defenseModule.damageReduction = baseStats.baseDamageReduction;

        return defenseModule;
    }

	public Character GetCharacter()
	{
		return playerCharacter;
	}

	public void AddCombatPlayerAbility(PlayerAbilityData ability)
	{
		combatPlayerAbilities.Add(ability);
	}

    public List<PlayerAbilityData> GetCombatAbilities()
    {
        return combatPlayerAbilities;
    }

	public void AddCombatPlayerAbilityModifier(PlayerAbilityModifierData modifier)
	{
		combatPlayerAbilityModifiers.Add(modifier);
	}

    public List<PlayerAbilityModifierData> GetCombatAbilityModifiers()
    {
        return combatPlayerAbilityModifiers;
    }
}
