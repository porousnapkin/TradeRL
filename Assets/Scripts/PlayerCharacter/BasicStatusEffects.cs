using UnityEngine;

public class BasicStatusEffects : ScriptableObject
{
    public static BasicStatusEffects Instance
    {
        get
        {
            return Resources.Load("BasicStatusEffects") as BasicStatusEffects;
        }
    }

    public StatusEffectData restedEffect;
}