using strange.extensions.mediation.impl;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeammateInfoList : DesertView {
    public TeammateInfoPanel teammateInfoPrefab;
    PlayerTeam playerTeam;
    List<GameObject> teamPanels = new List<GameObject>();

    public void Setup(PlayerTeam playerTeam)
    {
        this.playerTeam = playerTeam;

        playerTeam.TeamUpdatedEvent += RedrawTeam;
        RedrawTeam();
    }

    private void RedrawTeam()
    {
        teamPanels.ForEach(t => GameObject.Destroy(t));
        teamPanels.Clear();

        var teammateData = playerTeam.GetTeammateData();
        teammateData.ForEach(t =>
        {
            var go = GameObject.Instantiate(teammateInfoPrefab.gameObject, transform);
            var panel = go.GetComponent<TeammateInfoPanel>();
            panel.teammate = t;
        });
    }
}

public class TeammateInfoListMediator : Mediator
{
    [Inject] public TeammateInfoList view { private get; set; }
    [Inject] public PlayerTeam playerTeam { private get; set; }

    public override void OnRegister()
    {
        view.Setup(playerTeam);
    }
}
