using System;

public class EmptyAnimation : TargetedAnimation
{
    public void Play(Character target, Action finished, Action activated)
    {
        activated();
        finished();
    }
}