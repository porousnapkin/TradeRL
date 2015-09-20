using UnityEngine;
using UnityEngine.UI;

public class TownDialog : MonoBehaviour{
	public Text titleText;
	public Text descriptionText;
	public GameObject actionPrefab;
	public RectTransform actionParent;
	[HideInInspector]
	public Town townToRepresent;

	int numActions = 0;

	void Start() {
		//TODO: Haven't hooked up town to represent yet...
		//titleText.text = townToRepresent.name;

		//TODO: Towns don't have descriptions now, need to do this later...
		// descriptionText.text = townToRepresent.description;

		//TODO: How to data drive what actions we have? How to handle their input?
		CreateAction("Go to the inn and rest");
		CreateAction("Go to the tavern to gather information");
		CreateAction("Go to the market to sell goods");
		CreateAction("Prepare for a trading expedition");
	}

	void CreateAction(string actionDescription) {
		var go = GameObject.Instantiate(actionPrefab) as GameObject;
		go.transform.SetParent(actionParent, false);
		var rt = go.GetComponent<RectTransform>();
		rt.anchoredPosition = new Vector2(0, -(rt.rect.height / 2) - rt.rect.height * numActions);

		numActions++;
		var text = go.GetComponentInChildren<Text>();
		text.text = numActions.ToString() + ". " + actionDescription;
	}
}