using UnityEngine;
using UnityEngine.UI;

public class SkillStoryActionVisuals : MonoBehaviour {
	public TMPro.TextMeshProUGUI storyDescription;
	public TMPro.TextMeshProUGUI gameDescription;
	public TMPro.TextMeshProUGUI effortCostText;
	public Button effortButton;
	SkillStoryAction action;
	public System.Action ActivatedEvent = delegate{};
	public System.Action FinishedEvent = delegate{};

	public void Setup(SkillStoryAction action) {
		storyDescription.text = action.storyDescription;
		gameDescription.text = action.gameDescription;
		effortCostText.text = "Spend " + action.CalculateEffort() + " " + action.GetEffortType();
		this.action = action;

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
        effortButton.interactable = action.CanUse();
    }

	public void SpendEffortToSurpass() {
        ActivatedEvent();
		effortButton.onClick.RemoveAllListeners();

		if(action.CanUse()) 
			action.SucceedUsingEffort(FinishedEvent);
	}
}