using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class TownDialog : DesertView{
	public Text titleText;
	public GameObject actionPrefab;
	public RectTransform actionParent;

	public void SetupForTown(Town t) {
		titleText.text = t.name;
	}

	public void ClearPreviousActions() {
		foreach(Transform t in actionParent)
			GameObject.Destroy(t.gameObject);
	}

	public void SetupActionGO(GameObject actionGO, string actionDescription) {
		var go = GameObject.Instantiate(actionPrefab) as GameObject;
		go.transform.SetParent(actionParent, false);
		
		var text = go.GetComponentsInChildren<Text>(true)[0];
		text.text = actionDescription;

		actionGO.transform.SetParent(transform.parent, false);
		actionGO.SetActive(false);
		actionGO.GetComponent<CityActionDisplay>().SetReturnGameObject(gameObject);
		
		var button = go.GetComponent<Button>();
		button.onClick.AddListener(() => actionGO.SetActive(true));
		button.onClick.AddListener(() => gameObject.SetActive(false));
	}
}

public class TownDialogMediator : Mediator {
	[Inject] public TownDialog view {private get; set; }
	[Inject] public Town town {private get; set; }

	public override void OnRegister ()
	{
		view.SetupForTown(town);

		town.cityActionAddedEvent += CityActionAdded;

		LeanTween.delayedCall(0.0f, SetupActions);
	}

	public override void OnRemove ()
	{
		town.cityActionAddedEvent -= CityActionAdded;
	}
	
	void CityActionAdded(Town t, CityActionData ca) {
		SetupActions ();
	}

	void SetupActions() {
		view.ClearPreviousActions();

		foreach(var action in town.cityActions)
			CreateAction(action);
	}

	void CreateAction(CityActionData actionData) {
		if(actionData.isCityCenter)
			return;

		var cityActionGO = actionData.Create(town);
		view.SetupActionGO(cityActionGO, actionData.actionDescription);
	}
}
