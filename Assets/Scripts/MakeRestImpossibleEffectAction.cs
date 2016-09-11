public class MakeRestImpossibleEffectAction : EffectAction
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public void Apply()
    {
        playerCharacter.MakeRestingImpossible();
    }

    public bool CanCombine(EffectAction action)
    {
        return action is MakeRestImpossibleEffectAction;
    }

    public void Remove()
    {
        playerCharacter.AllowResting();
    }
}