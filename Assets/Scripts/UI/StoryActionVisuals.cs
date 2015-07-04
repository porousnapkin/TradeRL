using UnityEngine;
using UnityEngine.UI;

public class StoryActionVisuals : MonoBehaviour {
	public Text shortDescription;
	//What to do with long description?
	public Text chanceOfSuccessText;
	public Text effortCostText;
	StoryAction action;

	public event System.Action SuccessEvent = delegate{};
	public event System.Action FailedEvent = delegate{};

	public void Setup(StoryAction action) {
		shortDescription.text = action.shortDescription;
		chanceOfSuccessText.text = "Attempt \n" + (100 * action.chanceSuccess) + "%";
		effortCostText.text = "Effort \n" + action.effortToSurpass;
		this.action = action;
	}	

	public void Attempt() {
		if(action.Attempt())
			SuccessEvent();
		else
			FailedEvent();

		//Need visual and audible feedback on success or failure...
	}

	public void SpendEffortToSurpass() {
		if(action.CanAffordEffort())
			action.UseEffort();
		// else
			// Denote not enough effort...
	}
}