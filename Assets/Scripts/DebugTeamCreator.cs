using strange.extensions.mediation.impl;
using System.Collections.Generic;

public class DebugTeamCreator : DesertView {
    public List<AICharacterData> allies;
    public PlayerTeam playerTeam { private get; set; }

    public void Setup()
    {
        allies.ForEach(a => playerTeam.AddAlly(a));
    }

    public List<CombatController> CreateCombatAllies()
    {
        return playerTeam.CreateCombatAllies();
    }
}

public class DebugTeamCreatorMediator : Mediator
{
    [Inject] public DebugTeamCreator view { private get; set; }
    [Inject] public PlayerTeam playerTeam { private get; set; }

    public override void OnRegister()
    {
        view.playerTeam = playerTeam;
    }
}
