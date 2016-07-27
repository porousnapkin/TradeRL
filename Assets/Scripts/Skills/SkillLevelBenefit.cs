using UnityEngine;

public abstract class SkillLevelBenefit : ScriptableObject
{
	public abstract void Apply(PlayerCharacter playerCharacter);
}

