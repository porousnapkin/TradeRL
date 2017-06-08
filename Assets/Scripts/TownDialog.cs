using UnityEngine;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

public class TownDialog : DesertView{
	public TMPro.TextMeshProUGUI titleText;
	public GameObject actionPrefab;
	public RectTransform actionParent;
    public List<CityActionData> actions;
    public Town myTown;

    Dictionary<string, TownActionButton> actionNameToButton = new Dictionary<string, TownActionButton>();
    public const string cheatExpeditionName = "Travel";
    public const string cheatSellScreenName = "SellGoods";
    public const string cheatRestScreenName = "Rest";

	public void SetupForTown(Town t) {
		titleText.text = t.name;
        myTown = t;
	}

    public void SetupActions()
    {
        foreach(var actionData in actions)
        {
            if (actionData.isCityCenter)
                continue;

            if (actionNameToButton.ContainsKey(actionData.name))
                continue;

            var cityActionGO = actionData.Create(myTown);
            SetupActionGO(cityActionGO, actionData.actionDescription, actionData.name);
        }
    }

	public void SetupActionGO(GameObject actionGO, string actionDescription, string name) {
		var go = GameObject.Instantiate(actionPrefab) as GameObject;
		go.transform.SetParent(actionParent, false);
        var actionButton = go.GetComponent<TownActionButton>();

        actionButton.Setup(actionDescription, () =>
        {
            actionGO.SetActive(true);
            gameObject.SetActive(false);
        });
		
		actionGO.transform.SetParent(transform.parent, false);
		actionGO.SetActive(false);
		actionGO.GetComponent<CityActionDisplay>().SetReturnGameObject(gameObject);
		
        actionNameToButton[name] = actionButton;
    }

    public void SimulateButtonHitForAction(string actionName)
    {
        actionNameToButton[actionName].SimulateButtonHit();
    }

    public void NotifyNew(string name)
    {
        Debug.Log("Notifying new on " + name);
        actionNameToButton[name].NotifyNew();
    }
}

public class TownDialogMediator : Mediator {
	[Inject] public TownDialog view {private get; set; }
	[Inject] public Town town {private get; set; }

	public override void OnRegister ()
	{
		view.SetupForTown(town);

        town.playerActions.cityActionAddedEvent += CityActionAdded;
        town.playerActions.cityActionRemovedEvent += CityActionAdded;

        view.actions = town.playerActions.cityActions;
	}

	public override void OnRemove ()
	{
		town.playerActions.cityActionAddedEvent -= CityActionAdded;
        town.playerActions.cityActionRemovedEvent -= CityActionAdded;
	}
	
	void CityActionAdded(Town t, CityActionData ca) {
        view.actions = town.playerActions.cityActions;
        view.SetupActions();

        view.NotifyNew(ca.name);
	}
}
