using System.Collections.Generic;
using UnityEngine;
using System;

public class MultiActivatorAbility : AbilityActivator, Visualizer
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

    public void SetupVisualization(GameObject go)
    {
        activators.ForEach(a =>
        {
            if (a is Visualizer)
                (a as Visualizer).SetupVisualization(go);
        });
    }
}

