using UnityEngine;

public class StatusEffectData : ScriptableObject
{
    public string effectName;
    public string description;
    public EffectDurationData duration;
    public EffectActionData action;

    public StatusEffect Create(Character character)
    {
        var se = DesertContext.StrangeNew<StatusEffect>();
        se.name = effectName;
        se.description = description;
        se.duration = duration.Create(character);
        se.action = action.Create(character);
        return se;
    }
}