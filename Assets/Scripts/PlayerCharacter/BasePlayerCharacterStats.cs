using UnityEngine;
using System.Collections.Generic;

public class BasePlayerCharacterStats : ScriptableObject
{
    public static BasePlayerCharacterStats Instance
    {
        get
        {
            return Resources.Load("BasePlayerCharacterStats") as BasePlayerCharacterStats;
        }
    }

    public Sprite baseArt;
	public int maxHP = 10;
	public int baseMeleeDamage = 5;
	public int baseDamageReduction = 0;
	public int baseInitiative = 10;
	public int basePhysicalPool = 8;
	public int baseMentalPool = 8;
	public int baseSocialPool = 8;

	public List<PlayerAbilityData> defaultAbilities = new List<PlayerAbilityData>();
	public List<PlayerAbilityModifierData> defaultAbilityModifiers = new List<PlayerAbilityModifierData>();
    public List<AICharacterData> defaultAllies = new List<AICharacterData>();
}

