using System;

public class StatusEffect
{
    public string name { get; set; }
    public string description { get; set; }
    public EffectDuration duration { get; set; }
    public EffectAction action { get; set; }

    public event System.Action<StatusEffect> EffectFinished = delegate {};
    public event System.Action<StatusEffect> EffectUpdated = delegate {};

    public void Apply()
    {
        duration.Finished += DurationFinished;
        duration.Apply();
        action.Apply();
    }

    void DurationFinished()
    {
        EffectFinished(this);
    }

    public void Remove()
    {
        duration.Finished -= DurationFinished;
        action.Remove();
    }

    public bool AreTheSame(StatusEffect effect)
    {
        return action.CanCombine(effect.action);
    }

    public void CombineWith(StatusEffect effect)
    {
        duration.CombineWith(effect.duration);
    }
}