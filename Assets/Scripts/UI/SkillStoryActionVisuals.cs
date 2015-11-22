using UnityEngine;
using UnityEngine.UI;

public class SkillStoryActionVisuals : MonoBehaviour {
	public Text storyDescription;
	public Text gameDescription;
	public Text chanceOfSuccessText;
	public Text effortCostText;
	public Button attemptButton;
	public Button effortButton;
	SkillStoryAction action;
	public System.Action FinishedEvent = delegate{};

	public void Setup(SkillStoryAction action) {
		storyDescription.text = action.storyDescription;
		gameDescription.text = action.gameDescription;
		chanceOfSuccessText.text = (100 * action.chanceSuccess).ToString() + "% Chance";
		effortCostText.text = "Spend " + action.effortToSurpass + " Effort";
		this.action = action;

		attemptButton.onClick.AddListener(Attempt);
		effortButton.onClick.AddListener(SpendEffortToSurpass);
	}	

	public void Attempt() {
		attemptButton.onClick.RemoveAllListeners();
		effortButton.onClick.RemoveAllListeners();

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
		attemptButton.onClick.RemoveAllListeners();
		effortButton.onClick.RemoveAllListeners();

		if(action.CanAffordEffort()) {
			action.UseEffort();
		}

		FinishedEvent();
	}
}