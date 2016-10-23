using System;
using System.Collections.Generic;

public class MultiActivatorAbility : AbilityActivator
{
    public List<AbilityActivator> activators { private get; set; }
    int activatorIndex = 0;

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        activatorIndex = 0;
        ActivateNext(targets, animation, finishedAbility);
    }

    private void ActivateNext(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        if (activatorIndex >= activators.Count)
        {
            finishedAbility();
            return;
        }

        activators[activatorIndex].Activate(targets, animation, () =>
        {
            activatorIndex++;
            ActivateNext(targets, animation, finishedAbility);
        });
    }
}

