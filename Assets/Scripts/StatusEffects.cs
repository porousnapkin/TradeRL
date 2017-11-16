using System.Collections.Generic;

public class StatusEffects
{
    public const string PARTY = "PartyStatus";

    public event System.Action<StatusEffect> AddedNewEffect = delegate { };
    public event System.Action<StatusEffect> RemovedEffect = delegate { };
    List<StatusEffect> activeEffects = new List<StatusEffect>();

    public void AddStatusEffect(StatusEffect effect)
    {
        var existingEffect = activeEffects.Find(e => e.CanCombine(effect));
        if (existingEffect != null)
            existingEffect.CombineWith(effect);
        else
            SetupNewEffect(effect);
    }

    void SetupNewEffect(StatusEffect effect)
    {
        effect.Apply();
        activeEffects.Add(effect);
        effect.EffectFinished += RemoveEffect;

        AddedNewEffect(effect);
    }

    public void RemoveEffect(StatusEffect effect)
    {
        var toRemove = activeEffects.Find(e => e.CanCombine(effect));
        if (toRemove == null)
            return;

        toRemove.Remove();
        toRemove.EffectFinished -= RemoveEffect;
        activeEffects.Remove(toRemove);

        RemovedEffect(effect);
    }
}
