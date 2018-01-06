using System;
using strange.extensions.mediation.impl;
using TMPro;
using UnityEngine;

public class CityDetailsDisplay : DesertView
{
    public TextMeshProUGUI citizenReputationText;
    public TextMeshProUGUI politicalReputationText;
    public TextMeshProUGUI economyText;
    public Bar economyBar;
    public Bar citizenReputationBar;
    public Bar politicalReputationBar;
    public TownUpgradeDialog upgradeOptionsWindowPrefab;

    public CreateQuestButton createQuestButton;

    Town myTown;

    public void SetTown(Town t)
    {
        myTown = t;


        createQuestButton.town = t;



        //TODO: Visually account for levelups?
        myTown.citizensReputation.OnXPChanged += Redraw;
        myTown.politicalReputation.OnXPChanged += Redraw;
        myTown.economy.OnXPChanged += Redraw;

        myTown.citizensReputation.OnLevelChanged += CitizensReputation_OnLevelChanged;
        myTown.politicalReputation.OnLevelChanged += PoliticalReputation_OnLevelChanged;

        RedrawText();

        economyBar.SetInitialPercent(myTown.economy.GetPercentToNextLevel());
        citizenReputationBar.SetInitialPercent(myTown.citizensReputation.GetPercentToNextLevel());
    }


    private void PoliticalReputation_OnLevelChanged()
    {
        var windowGO = GameObject.Instantiate(upgradeOptionsWindowPrefab.gameObject, transform.parent.parent) as GameObject;
        windowGO.GetComponent<TownUpgradeDialog>().SetupPoliticalInfluenceUpgrade(myTown);
    }

    private void CitizensReputation_OnLevelChanged()
    {
        var windowGO = GameObject.Instantiate(upgradeOptionsWindowPrefab.gameObject, transform.parent.parent) as GameObject;
        windowGO.GetComponent<TownUpgradeDialog>().SetupCitizenInfluenceUpgrade(myTown);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();


        myTown.citizensReputation.OnXPChanged -= Redraw;
        myTown.politicalReputation.OnXPChanged -= Redraw;
        myTown.citizensReputation.OnLevelChanged -= CitizensReputation_OnLevelChanged;
        myTown.politicalReputation.OnLevelChanged -= PoliticalReputation_OnLevelChanged;
        myTown.economy.OnXPChanged -= Redraw;
    }


    void Redraw()
    {
        RedrawText();
        RedrawBars();
    }

    private void RedrawBars()
    {
        economyBar.SetPercent(myTown.economy.GetPercentToNextLevel());
        citizenReputationBar.SetPercent(myTown.citizensReputation.GetPercentToNextLevel());
        politicalReputationBar.SetPercent(myTown.politicalReputation.GetPercentToNextLevel());
    }

    void RedrawText()
    {
        citizenReputationText.text = "Level " + myTown.citizensReputation.GetLevel();
        politicalReputationText.text = "Level " + myTown.politicalReputation.GetLevel();
        economyText.text = "Level " + myTown.economy.GetLevel();
    }
}

public class CityDetailsDisplayMediator : Mediator
{
	[Inject] public CityDetailsDisplay view { private get; set; }
	[Inject] public Town myTown { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        view.SetTown(myTown);
    }
}