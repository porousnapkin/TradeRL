using UnityEngine;
using UnityEngine.UI;

public class SkillStoryActionVisuals : MonoBehaviour {
	public Text shortDescription;
	//What to do with long description?
	public Text chanceOfSuccessText;
	public Text effortCostText;
	SkillStoryAction action;
	public System.Action FinishedEvent = delegate{};

	public void Setup(SkillStoryAction action) {
		shortDescription.text = action.shortDescription;
		chanceOfSuccessText.text = "Attempt \n" + (100 * action.chanceSuccess) + "%";
		effortCostText.text = "Effort \n" + action.effortToSurpass;
		this.action = action;
	}	

	public void Attempt() {
		action.Attempt();

		//TODO
		// if(action.Attempt())
		//  SuccessVisuals();
		// else
		// 	FailureVisuals();

		//Need visual and audible feedback on success or failure...
		
		FinishedEvent();
	}

	public void SpendEffortToSurpass() {
		if(action.CanAffordEffort()) {
			action.UseEffort();
		}
		// else
			// Denote not enough effort...

		FinishedEvent();
	}
}