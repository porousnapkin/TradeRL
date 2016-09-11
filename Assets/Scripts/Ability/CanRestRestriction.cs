public class CanRestRestriction : Restriction
{
    [Inject] public PlayerCharacter playerCharacter { private get; set; }

    public bool CanUse()
    {
        UnityEngine.Debug.Log("Checking that we can use rest. " + playerCharacter.CanRest());
        return playerCharacter.CanRest();
    }
}