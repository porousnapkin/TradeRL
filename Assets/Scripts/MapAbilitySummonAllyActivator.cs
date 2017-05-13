public class MapAbilitySummonAllyActivator : MapAbilityActivator
{
    [Inject]
    public PlayerTeam playerTeam { private get; set; }
    public AICharacterData character { private get; set; }

    public void Activate(System.Action callback)
    {
        //TODO: Summoned allies shouldn't have a wounded state, should they?
        playerTeam.AddAlly(character);
    }
}
