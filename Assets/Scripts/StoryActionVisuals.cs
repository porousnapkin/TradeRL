using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryActionVisuals : MonoBehaviour {
	public Text storyDescription;
	public Text gameplayDescription;
	public Button button;
	public event System.Action FinishedEvent = delegate{};
	public List<StoryActionEvent> actionEvents;
    public List<Restriction> restrictions = new List<Restriction>();

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

		foreach(var e in actionEvents)
			e.Activate();

		FinishedEvent();
	}
}