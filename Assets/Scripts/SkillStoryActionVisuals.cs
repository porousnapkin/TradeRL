using UnityEngine;
using UnityEngine.UI;

public class SkillStoryActionVisuals : MonoBehaviour {
	public TMPro.TextMeshProUGUI storyDescription;
	public TMPro.TextMeshProUGUI gameDescription;
	public TMPro.TextMeshProUGUI chanceOfSuccessText;
	public TMPro.TextMeshProUGUI effortCostText;
	public Button attemptButton;
	public Button effortButton;
	SkillStoryAction action;
	public System.Action FinishedEvent = delegate{};

	public void Setup(SkillStoryAction action) {
		storyDescription.text = action.storyDescription;
		gameDescription.text = action.gameDescription;
		chanceOfSuccessText.text = (100 * action.CalculateChanceOfSuccess()).ToString() + "% Chance";
		effortCostText.text = "Spend " + action.CalculateEffort() + " " + action.GetEffortType();
		this.action = action;

		attemptButton.onClick.AddListener(Attempt);
		effortButton.onClick.AddListener(SpendEffortToSurpass);
        CheckUsability();
	}

    void OnEnable()
    {
        if(action != null)
            CheckUsability();
    }

    void CheckUsability()
    {
        var canUse = action.CanUse();
        attemptButton.interactable = canUse;
        effortButton.interactable = canUse && action.CanAffordEffort();
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