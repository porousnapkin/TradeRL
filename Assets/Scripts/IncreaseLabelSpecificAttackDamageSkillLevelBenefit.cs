using System.Collections.Generic;
using UnityEngine;

public class IncreaseLabelSpecificAttackDamageSkillLevelBenefit : SkillLevelBenefit
{
    public List<AbilityLabel> labelRequirements;
    public int damageAmount;
    public string modName;

    public override void Apply(PlayerCharacter playerCharacter)
    {
        var bonus = playerCharacter.GetCharacter().attackModule.attackModifierSet.GetActiveModifier(modName, BonusConstructor) as LabelRequiredAttackBonus;
        bonus.bonusDamage += damageAmount;
    }

    LabelRequiredAttackBonus BonusConstructor()
    {
        var bonus = DesertContext.StrangeNew<LabelRequiredAttackBonus>();
        bonus.labelRequirements = labelRequirements;
        bonus.name = modName;
        return bonus;
    }
}

