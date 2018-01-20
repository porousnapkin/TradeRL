using System;
using System.Collections.Generic;

public class ShieldGeneratorAbilityModifier : AbilityModifier
{
    public int shieldAmount;

    public void PrepareActivation(List<Character> targets, Action callback)
    {
        callback();
    }

    public void ActivationEnded(List<Character> targets, Action callback)
    {
        targets.ForEach(t =>
        {
            var shieldMod = ShieldDefenseMod.GetFrom(t);
            shieldMod.Value += shieldAmount;
        });

        callback();
    }

    public void BeforeActivation(List<Character> targets, Action callback)
    {
        callback();
    }
}

