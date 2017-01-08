using System;
using System.Collections.Generic;


public class MultiAnimation : TargetedAnimation
{
    public enum HowToPlay
    {
        AllAtOnce,
        OneAtATime,
    }
    public HowToPlay howToPlay;
    public List<TargetedAnimation> animations;
    int animationsActivated;
    int animationsFinished;
    Action finished;
    Action activated;
    int index;
    Character target;

    public void Play(Character target, Action finished, Action activated)
    {
        this.finished = finished;
        this.activated = activated;
        this.target = target;

        if (howToPlay == HowToPlay.AllAtOnce)
            PlayAllAtOnce();
        else
            PlayOneAtATime();
    }

    void PlayOneAtATime()
    {
        index = -1;
        animationsActivated = 0;
        PlayNextAnimation();
    }

    private void PlayNextAnimation()
    {
        if (index >= animations.Count)
            finished();

        index++;
        animations[index].Play(target, PlayNextAnimation, CountUpActivations);
    }

    void PlayAllAtOnce()
    {
        animationsActivated = 0;
        animationsFinished = 0;

        animations.ForEach(a => a.Play(target, AllAtOnceFinished, CountUpActivations));
    }

    void CountUpActivations()
    {
        animationsActivated++;
        if (animationsActivated >= animations.Count)
            activated();
    }

    void AllAtOnceFinished()
    {
        animationsFinished++;
        if (animationsFinished >= animations.Count)
            finished();
    }
}

