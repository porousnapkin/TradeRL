using UnityEngine;
using UnityEngine.UI;

public class StoryActionVisuals : MonoBehaviour {
	public Text shortDescription;
	//What to do with long description?
	public Text chanceOfSuccessText;
	public Text effortCostText;
	StoryAction action;

	public void Setup(StoryAction action) {
		shortDescription.text = action.shortDescription;
		chanceOfSuccessText.text = "Attempt \n" + (100 * action.chanceSuccess) + "%";
		effortCostText.text = "Effort \n" + action.effortToSurpass;
		this.action = action;
	}	

	public void Attempt() {
		action.Attempt();
	}

	public void SpendEffortToSurpass() {
		action.UseEffort();
	}
}