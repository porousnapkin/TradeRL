using strange.extensions.mediation.impl;
using UnityEngine;

public class HireAlliesScreen : CityActionDisplay
{
    public Transform panelParent;
    public HireTeammatePanel hireablePanel;
	Town town;

    public void Setup(Town town)
    {
        this.town = town;

        town.hireableAllies.ForEach(a => CreatePanel(a));
    }

    private void CreatePanel(HireableAllyData a)
    {
        var hireableGO = GameObject.Instantiate(hireablePanel, panelParent);
        var panel = hireableGO.GetComponent<HireTeammatePanel>();
        panel.SetupAlly(a);
    }
}

public class HireAlliesScreenMediator : Mediator
{
    [Inject] public Town town { private get; set; }
    [Inject] public HireAlliesScreen view { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        view.Setup(town);
    }
}
