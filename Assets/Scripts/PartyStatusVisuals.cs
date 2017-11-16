using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine.UI;

public class PartyStatusVisuals : DesertView
{
    public Text text;
    List<StatusEffect> activeEffects = new List<StatusEffect>();

    protected override void Start()
    {
        base.Start();

        RedrawText();
    }

    public void AddStatusEffect(StatusEffect effect)
    {
        activeEffects.Add(effect);
        effect.EffectUpdated += EffectUpdated;

        RedrawText();
    }

    void EffectUpdated(StatusEffect effect)
    {
        RedrawText();
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
        effect.EffectUpdated -= EffectUpdated;

        RedrawText();
    }

    void RedrawText()
    {
        if (activeEffects.Count == 0)
        {
            text.text = "";
            return;
        }

        text.text = "PartyStatus:";
        activeEffects.ForEach(e =>
        {
            text.text += "\n" + GetEffectString(e);
        });
    }

    string GetEffectString(StatusEffect effect)
    {
        return effect.name + " (" + effect.duration.PrettyPrint() + ")";
    } 
}

public class PartyStatusMediator : Mediator
{
    [Inject] public PartyStatusVisuals view { get; set; }
    [Inject(StatusEffects.PARTY)] public StatusEffects model { get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        model.AddedNewEffect += view.AddStatusEffect;
        model.RemovedEffect += view.RemoveStatusEffect;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        model.AddedNewEffect -= view.AddStatusEffect;
        model.RemovedEffect -= view.RemoveStatusEffect;
    }
}
