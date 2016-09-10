public interface EffectDuration
{
    event System.Action Finished;

    void CombineWith(EffectDuration duration);
    void Apply();
}