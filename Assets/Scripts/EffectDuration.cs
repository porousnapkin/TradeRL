public interface EffectDuration
{
    event System.Action Finished;
    event System.Action Updated;

    void CombineWith(EffectDuration duration);
    void Apply();
    string PrettyPrint();
}