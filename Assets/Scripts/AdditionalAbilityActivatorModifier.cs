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

    public void BeforeActivation(List<Character> targets, System.Action callback )
    {
        if (whenToActivate == WhenToActivate.BeforeAbility)
            additionalActivator.Activate(targets, animation, callback);
        else
            callback();
    }

    public void ActivationEnded(List<Character> targets, System.Action callback)
    {
        if(whenToActivate == WhenToActivate.AfterAbility)
            additionalActivator.Activate(targets, animation, callback);
        else
            callback();
    }

    public void PrepareActivation(List<Character> targets, System.Action callback)
    {
        callback();
    }
}

