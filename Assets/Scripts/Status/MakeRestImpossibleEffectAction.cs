public class MakeRestImpossibleEffectAction : EffectAction
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public void Apply()
    {
        playerCharacter.MakeRestingImpossible();
        UnityEngine.Debug.Log("Making resting impossible");
    }

    public bool CanCombine(EffectAction action)
    {
        return action is MakeRestImpossibleEffectAction;
    }

    public void Remove()
    {
        playerCharacter.AllowResting();
        UnityEngine.Debug.Log("allowing resting");
    }
}