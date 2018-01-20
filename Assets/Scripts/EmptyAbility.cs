using System;
using System.Collections.Generic;

public class EmptyAbility : AbilityActivator
{
    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        finishedAbility();
    }

    public void PrepareActivation(List<Character> targets, TargetedAnimation animation, Action preparedCallback)
    {
        preparedCallback();
    }
}