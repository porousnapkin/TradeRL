using System;
using strange.extensions.mediation.impl;
using TMPro;

public class CityDetailsDisplay : DesertView
{
    public TextMeshProUGUI citizenReputationText;
    public TextMeshProUGUI economyText;
    public Bar economyBar;
    public Bar citizenReputationBar;
    Town myTown;

    public void SetTown(Town t)
    {
        myTown = t;
        myTown.citizensReputation.OnXPChanged += Redraw;
        myTown.economy.OnXPChanged += Redraw;

        RedrawText();

        economyBar.SetInitialPercent(myTown.economy.GetPercentToNextLevel());
        citizenReputationBar.SetInitialPercent(myTown.citizensReputation.GetPercentToNextLevel());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        myTown.citizensReputation.OnXPChanged -= Redraw;
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
    }

    void RedrawText()
    {
        citizenReputationText.text = "Level " + myTown.citizensReputation.GetLevel();
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