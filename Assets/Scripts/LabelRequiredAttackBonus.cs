using System.Collections.Generic;

public class LabelRequiredAttackBonus
{
    [Inject] public ActiveLabelRequirements labels { private get; set; }
    public int bonusDamage { private get; set; }
    public List<AbilityLabel> labelRequirements { private get; set; }
    AttackModule activeAttackModule;

    public void Apply(AttackModule module)
    {
        module.modifyOutgoingAttack += ModifyAttack;
        activeAttackModule = module;
    }

    public void Remove()
    {
        activeAttackModule.modifyOutgoingAttack -= ModifyAttack;
    }

    void ModifyAttack(AttackData attackData)
    {
        if (labelRequirements.TrueForAll(l => labels.GetActiveLabels().Contains(l)))
            attackData.baseDamage += bonusDamage;
    }
}

