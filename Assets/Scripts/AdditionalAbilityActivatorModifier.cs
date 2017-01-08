using System.Collections.Generic;

public class AdditionalAbilityActivatorModifier : AbilityModifier
{
    public enum WhenToActivate
    {
        BeforeAbility,
        AfterAbility,
    }
    public WhenToActivate whenToActivate;
    public AbilityActivator additionalActivator;
    public TargetedAnimation animation;

    public void BeforeActivation(CombatController owner, List<Character> targets)
    {
        if(whenToActivate == WhenToActivate.BeforeAbility)
            additionalActivator.Activate(targets, animation, () => { });
    }

    public void ActivationEnded(CombatController owner, List<Character> targets)
    {
        if(whenToActivate == WhenToActivate.AfterAbility)
            additionalActivator.Activate(targets, animation, () => { });
    }
}

