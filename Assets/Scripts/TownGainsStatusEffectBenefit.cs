
public class TownGainsStatusEffectBenefit : TownBenefit
{
    public StatusEffect statusEffect { private get; set; }

    public void Apply()
    {
        statusEffect.Apply();
    }

    public void Undo()
    {
        statusEffect.Remove();
    }
}

