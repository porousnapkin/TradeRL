using System.Collections.Generic;

public class LabelRequiredAttackBonus : AttackModifier
{
    [Inject] public ActiveLabelRequirements labels { private get; set; }
    public int bonusDamage = 0;
    public List<AbilityLabel> labelRequirements = new List<AbilityLabel>();
    AttackModule activeAttackModule;

    public void ModifyAttack(AttackData attackData)
    {
        if (labelRequirements.TrueForAll(l => labels.GetActiveLabels().Contains(l)))
            attackData.baseDamage += bonusDamage;
    }
}

