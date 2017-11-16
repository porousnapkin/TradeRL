public interface EffectAction
{
    void Remove();
    void Apply();
    bool CanCombine(EffectAction action);
}

