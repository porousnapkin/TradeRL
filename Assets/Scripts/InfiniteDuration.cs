using System;

public class InfiniteDuration : EffectDuration
{
    public event Action Finished = delegate { };
    public event Action Updated = delegate { };

    public void Apply()
    {
    }

    public void CombineWith(EffectDuration duration)
    {
    }

    public string PrettyPrint()
    {
        return "";
    }
}

