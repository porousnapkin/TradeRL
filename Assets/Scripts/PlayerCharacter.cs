﻿using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter {
	[Inject] public PlayerSkills skills { private get; set; }

	List<PlayerAbilityData> combatPlayerAbilities = new List<PlayerAbilityData>();
	List<PlayerAbilityModifierData> combatPlayerAbilityModifiers = new List<PlayerAbilityModifierData>();
	Character playerCharacter;
    Sprite characterArt;
    bool canRest = true;
    int spotBonus = 0;

    public PlayerCharacter()
    {
        BuildBasics();
    }

    void BuildBasics()
    {
        playerCharacter = DesertContext.StrangeNew<Character>();

        var baseStats = BasePlayerCharacterStats.Instance;
        SetArt(baseStats.baseArt);
        playerCharacter.Setup(baseStats.maxHP);
        playerCharacter.attackModule = CreateAttackModule(baseStats);
        playerCharacter.defenseModule = CreateDefenseModule(baseStats);
        playerCharacter.speed = baseStats.baseInitiative;
        playerCharacter.myFaction = Faction.Player;
        playerCharacter.displayName = "<color=Orange>" + "PLAYA" + "</color>";

        combatPlayerAbilities.Clear();
        combatPlayerAbilityModifiers.Clear();
        baseStats.defaultAbilities.ForEach(a => AddCombatPlayerAbility(a));
        baseStats.defaultAbilityModifiers.ForEach(m => AddCombatPlayerAbilityModifier(m));
    }

    public void BuildCharacter()
    {
        BuildBasics();

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

    public void SetArt(Sprite art)
    {
        this.characterArt = art;
    }

    public Sprite GetArt()
    {
        return characterArt;
    }

    public bool CanRest()
    {
        return canRest;
    }

    public void MakeRestingImpossible()
    {
        canRest = false;
    }

    public void AllowResting()
    {
        canRest = true;
    }

    public int GetSpotBonus()
    {
        return spotBonus;
    }
}
