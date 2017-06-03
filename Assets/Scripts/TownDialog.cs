using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using strange.extensions.mediation.impl;
using System.Collections.Generic;

public class TownDialog : DesertView{
	public TMPro.TextMeshProUGUI titleText;
	public GameObject actionPrefab;
	public RectTransform actionParent;
    public List<CityActionData> actions;
    public Town myTown;

    Dictionary<string, Button> actionNameToButton = new Dictionary<string, Button>();
    public const string cheatExpeditionName = "Travel";
    public const string cheatSellScreenName = "SellGoods";
    public const string cheatRestScreenName = "Rest";

	public void SetupForTown(Town t) {
		titleText.text = t.name;
        myTown = t;
	}

	public void ClearPreviousActions() {
		foreach(Transform t in actionParent)
			GameObject.Destroy(t.gameObject);
	}

    public void SetupActions()
    {
        foreach(var actionData in actions)
        {
            if (actionData.isCityCenter)
                continue;

            var cityActionGO = actionData.Create(myTown);
            SetupActionGO(cityActionGO, actionData.actionDescription, actionData.name);
        }

    }

	public void SetupActionGO(GameObject actionGO, string actionDescription, string name) {
		var go = GameObject.Instantiate(actionPrefab) as GameObject;
		go.transform.SetParent(actionParent, false);
		
		var text = go.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true)[0];
		text.text = actionDescription;

		actionGO.transform.SetParent(transform.parent, false);
		actionGO.SetActive(false);
		actionGO.GetComponent<CityActionDisplay>().SetReturnGameObject(gameObject);
		
		var button = go.GetComponent<Button>();
		button.onClick.AddListener(() => actionGO.SetActive(true));
		button.onClick.AddListener(() => gameObject.SetActive(false));
        actionNameToButton[name] = button;
    }

    public void SimulateButtonHitForAction(string actionName)
    {
        var action = actionNameToButton[actionName];
        var pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(action.gameObject, pointer, ExecuteEvents.pointerClickHandler);
    }
}

public class TownDialogMediator : Mediator {
	[Inject] public TownDialog view {private get; set; }
	[Inject] public Town town {private get; set; }

	public override void OnRegister ()
	{
		view.SetupForTown(town);

        town.playerActions.cityActionAddedEvent += CityActionAdded;

		SetupActions();
	}

	public override void OnRemove ()
	{
		town.playerActions.cityActionAddedEvent -= CityActionAdded;
	}
	
	void CityActionAdded(Town t, CityActionData ca) {
		SetupActions ();
        view.SetupActions();
	}

	void SetupActions() {
		view.ClearPreviousActions();

        view.actions = town.playerActions.cityActions;
	}
}
