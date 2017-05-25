using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryActionVisuals : MonoBehaviour {
	public TMPro.TextMeshProUGUI storyDescription;
	public TMPro.TextMeshProUGUI gameplayDescription;
	public Button button;
	public event System.Action FinishedEvent = delegate{};
	public List<StoryActionEvent> actionEvents;
    public List<Restriction> restrictions = new List<Restriction>();
    int finishedActions = 0;

	public void Setup(string storyDescription, string gameplayDescription) {
		this.storyDescription.text = storyDescription;
		this.gameplayDescription.text = gameplayDescription;

		button.onClick.AddListener(Use);
        CheckUsability();
	}

    void OnEnable()
    {
        CheckUsability();
    }

    void CheckUsability()
    {
        button.interactable = restrictions.TrueForAll(r => r.CanUse());
    }

	public void Use() {
		button.onClick.RemoveAllListeners();

        finishedActions = 0;
        foreach (var e in actionEvents)
			e.Activate(CountActions);
	}

    void CountActions()
    {
        finishedActions++;
        if (finishedActions >= actionEvents.Count)
            FinishedEvent();
    }
}