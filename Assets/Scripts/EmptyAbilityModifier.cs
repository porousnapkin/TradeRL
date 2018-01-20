using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class EmptyAbilityModifier : AbilityModifier
{
    public void ActivationEnded(List<Character> targets, Action callback)
    {
        callback();
    }

    public void BeforeActivation(List<Character> targets, Action callback)
    {
        callback();
    }

    public void PrepareActivation(List<Character> targets, Action callback)
    {
        callback();
    }
}

