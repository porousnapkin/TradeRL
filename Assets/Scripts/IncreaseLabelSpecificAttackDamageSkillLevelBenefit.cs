using System;
using System.Collections.Generic;

public class IncreaseLabelSpecificAttackDamageSkillLevelBenefit : SkillLevelBenefit
{
    public List<AbilityLabel> labelRequirements;
    public int damageAmount;

    public override void Apply(PlayerCharacter playerCharacter)
    {
        var bonus = DesertContext.StrangeNew<LabelRequiredAttackBonus>();
        bonus.bonusDamage = damageAmount;
        bonus.labelRequirements = labelRequirements;
        bonus.Apply(playerCharacter.GetCharacter().attackModule);
    }
}

