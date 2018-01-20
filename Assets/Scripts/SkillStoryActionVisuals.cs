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
	public System.Action ActivatedEvent = delegate{};
	public System.Action FinishedEvent = delegate{};

	public void Setup(SkillStoryAction action) {
		storyDescription.text = action.storyDescription;
		gameDescription.text = action.gameDescription;
		chanceOfSuccessText.text = (100 * action.CalculateChanceOfSuccess()).ToString("N0") + "% Chance";
		effortCostText.text = "Spend " + action.CalculateEffort() + " " + action.GetEffortType();
		this.action = action;

		attemptButton.onClick.AddListener(() => Attempt(() => FinishedEvent()));
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

	public void Attempt(System.Action callback) {
        ActivatedEvent();
        attemptButton.onClick.RemoveAllListeners();
		effortButton.onClick.RemoveAllListeners();

		action.Attempt(callback);

		//TODO
		// if(action.Attempt())
		//  SuccessVisuals();
		// else
		// 	FailureVisuals();

		//Need visual and audible feedback on success or failure...
		
	}

	public void SpendEffortToSurpass() {
        ActivatedEvent();
		attemptButton.onClick.RemoveAllListeners();
		effortButton.onClick.RemoveAllListeners();

		if(action.CanAffordEffort()) {
			action.UseEffort(() => { FinishedEvent(); });
		}
	}
}