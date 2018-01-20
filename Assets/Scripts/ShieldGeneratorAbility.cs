using System;
using System.Collections.Generic;

public class ShieldGeneratorAbility : AbilityActivator
{
    public int shieldAmount;

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        targets.ForEach(t =>
        {
            var shieldMod = ShieldDefenseMod.GetFrom(t);
            shieldMod.Value += shieldAmount;
        });

        finishedAbility();
    }

    public void PrepareActivation(List<Character> targets, TargetedAnimation animation, Action preparedCallback)
    {
        preparedCallback();
    }
}

