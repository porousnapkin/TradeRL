using UnityEngine;
using UnityEngine.UI;

public class TownDialog : MonoBehaviour{
	public Text titleText;
	public GameObject actionPrefab;
	public RectTransform actionParent;
	[HideInInspector]public Town townToRepresent;

	void Start() {
		titleText.text = townToRepresent.name;

		SetupActions();
		townToRepresent.cityActionAddedEvent += CityActionAdded;
	}

	void OnDestroy() {
		townToRepresent.cityActionAddedEvent -= CityActionAdded;
	}

	void CityActionAdded(Town t, CityAction ca) {
		SetupActions ();
	}

	void SetupActions() {
		foreach(Transform t in actionParent)
			GameObject.Destroy(t.gameObject);
		foreach(var action in townToRepresent.cityActions)
			CreateAction(action);
	}

	string GetCityActionDescription(CityAction a) {
		switch(a) {
			case CityAction.Market:
				return "Find the local markets";
			case CityAction.Inn:
				return "Look for a place to rest";
			case CityAction.Travel:
				return "Prepare for an expedition";
			case CityAction.Pub:
				return "Gather information";
			case CityAction.BuldingScene:
				return "Create buildings";
			default:
				return "";
		}
	}

	void CreateAction(CityAction action) {
		if(action == CityAction.Center)
			return;

		var go = GameObject.Instantiate(actionPrefab) as GameObject;
		go.transform.SetParent(actionParent, false);

		var text = go.GetComponentsInChildren<Text>(true)[0];
		text.text = GetCityActionDescription(action);

		var actionGO = CityActionFactory.CreateCityAction(action, townToRepresent);
		actionGO.transform.SetParent(transform.parent, false);
		actionGO.SetActive(false);
		actionGO.GetComponent<CityActionDisplay>().SetReturnGameObject(gameObject);

		var button = go.GetComponent<Button>();
		button.onClick.AddListener(() => actionGO.SetActive(true));
		button.onClick.AddListener(() => gameObject.SetActive(false));
	}
}