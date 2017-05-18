using System.Collections.Generic;

public class CounterAttackSkillLevelBenefit : SkillLevelBenefit
{
    public int minDamage = 4;
    public int maxDamage = 14;
    public bool canCrit = false;
    public bool randomlyActivates;
    public float randomActivationChance;
    public bool onlyCountersMeleeAttacks;
    public List<AbilityLabel> labels;

    public override void Apply(PlayerCharacter playerCharacter)
    {
        var counterAttack = DesertContext.StrangeNew<CounterAttack>();
        counterAttack.canCrit = canCrit;
        counterAttack.minDamage = minDamage;
        counterAttack.maxDamage = maxDamage;
        counterAttack.randomlyActivates = randomlyActivates;
        counterAttack.randomActivationChance = randomActivationChance;
        counterAttack.onlyCountersMeleeAttacks = onlyCountersMeleeAttacks;
        counterAttack.labels = labels;

        playerCharacter.GetCharacter().attackModule.AddCounterAttack(counterAttack);
    }
}
