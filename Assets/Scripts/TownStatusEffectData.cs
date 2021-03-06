using UnityEngine;


public class TownStatusEffectData : ScriptableObject
{
    public string effectName;
    public string description;
    public EffectDurationData duration;
    public TownEffectActionData action;

    public StatusEffect Create(Town t)
    {
        var se = DesertContext.StrangeNew<StatusEffect>();
        se.name = effectName;
        se.description = description;
        se.duration = duration.Create();
        se.action = action.Create(t);
        return se;
    }
}

