using System;
using System.Collections.Generic;

public class AbilityAndAbilityModifierAbility : AbilityActivator
{
    public List<AbilityModifier> modifiers { private get; set; }
    public AbilityActivator ability { private get; set; }
    int modCount = 0;
    List<Character> targets;
    TargetedAnimation animation;
    Action finishedAbility;

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        this.targets = targets;
        this.animation = animation;
        this.finishedAbility = finishedAbility;
        modCount = 0;
        modifiers.ForEach(m => m.BeforeActivation(targets, CountBefore));
    }

    private void CountBefore()
    {
        modCount++;
        if (modCount >= modifiers.Count)
            ability.Activate(targets, animation, AbilityFinished);
    }

    private void AbilityFinished()
    {
        modCount = 0;
        modifiers.ForEach(m => m.ActivationEnded(targets, CountAfter));
    }

    private void CountAfter()
    {
        modCount++;
        if (modCount >= modifiers.Count)
            finishedAbility();
    }
}
