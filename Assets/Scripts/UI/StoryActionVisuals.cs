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
		chanceOfSuccessText.text = action.chanceSuccess.ToString();
		effortCostText.text = action.effortToSurpass.ToString();
		this.action = action;
	}	

	public void Attempt() {
		action.Attempt();
	}

	public void SpendEffortToSurpass() {
		action.UseEffort();
	}
}