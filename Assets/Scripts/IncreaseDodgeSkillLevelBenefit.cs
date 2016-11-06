using UnityEngine;

public class IncreaseDodgeSkillLevelBenefit : SkillLevelBenefit
{
    public float dodgeGain = 0.15f;
    public string modName = "Dodge";

    public override void Apply(PlayerCharacter playerCharacter)
    {
        var bonus = (DodgeDefenseMod)playerCharacter.GetCharacter().defenseModule.attackModifierSet.GetActiveModifier(modName, BonusConstructor);
        bonus.dodgeChance += Mathf.Min(dodgeGain, 0.9f);
    }

    DodgeDefenseMod BonusConstructor()
    {
        var bonus = DesertContext.StrangeNew<DodgeDefenseMod>();
        return bonus;
    }
}

