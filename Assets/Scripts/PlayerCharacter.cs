using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter {
	[Inject] public PlayerSkills skills { private get; set; }
    [Inject] public PlayerAmbushButtons ambushButtons { private get; set; }

    public event System.Action abilitiesChanged = delegate { };
    public event System.Action abilityModifiersChanged = delegate { };

	List<PlayerAbilityData> combatPlayerAbilities = new List<PlayerAbilityData>();
	List<PlayerAbilityModifierData> combatPlayerAbilityModifiers = new List<PlayerAbilityModifierData>();
    List<PlayerAbilityData> ambushPlayerAbilities = new List<PlayerAbilityData>();
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
        playerCharacter.displayName = "PLAYA";

        combatPlayerAbilities.Clear();
        combatPlayerAbilities.Add(CombatReferences.Get().emptyAbility);
        combatPlayerAbilityModifiers.Clear();
        ambushPlayerAbilities.Clear();
        baseStats.defaultAbilities.ForEach(a => AddCombatPlayerAbility(a));
        baseStats.defaultAbilityModifiers.ForEach(m => AddCombatPlayerAbilityModifier(m));
        baseStats.defaultAmbushAbilities.ForEach(a => AddAmbushAbility(a));
    }

    public void BuildCharacter()
    {
        skills.ReapplyAllSkills();
    }

    AttackModule CreateAttackModule(BasePlayerCharacterStats baseStats)
    {
        var attackModule = new AttackModule();
        attackModule.minDamage = baseStats.baseMinMeleeDamage;
        attackModule.maxDamage = baseStats.baseMaxMeleeDamage;

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
        abilitiesChanged();
	}

    public void RemoveCombatPlayerAbility(PlayerAbilityData ability)
    {
        combatPlayerAbilities.Remove(ability);
        abilitiesChanged();
    }

    public List<PlayerAbility> GetCombatAbilities(CombatController controller)
    {
        return combatPlayerAbilities.ConvertAll(a => a.Create(controller));
    }

	public void AddCombatPlayerAbilityModifier(PlayerAbilityModifierData modifier)
	{
		combatPlayerAbilityModifiers.Add(modifier);
        abilityModifiersChanged();
	}

    public void RemoveCombatPlayerAbilityModifier(PlayerAbilityModifierData modifier)
    {
		combatPlayerAbilityModifiers.Remove(modifier);
        abilityModifiersChanged();
    }

    public List<PlayerAbilityModifier> GetCombatAbilityModifiers(CombatController controller)
    {
        return combatPlayerAbilityModifiers.ConvertAll(c => c.Create(controller));
    }

    public void AddAmbushAbility(PlayerAbilityData ability)
    {
        ambushPlayerAbilities.Add(ability);
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

	public void SetSpotBonus(int spotBonus) 
	{
		this.spotBonus = spotBonus;
	}

    public void PickAmbush(System.Action callback)
    {
        if (ambushPlayerAbilities.Count == 0)
        {
            callback();
            return;
        }

        var createdAmbushes = ambushPlayerAbilities.ConvertAll(a => a.Create(playerCharacter.controller));
        createdAmbushes.Add(CombatReferences.Get().emptyAbility.Create(playerCharacter.controller));
        ambushButtons.Setup(createdAmbushes, (a) => a.Activate(callback));
    }
}
