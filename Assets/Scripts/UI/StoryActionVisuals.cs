using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryActionVisuals : MonoBehaviour {
	public Text shortDescription;
	//What to do with long description?
	public System.Action FinishedEvent = delegate{};
	public List<StoryActionEvent> actionEvents;

	public void Setup(string shortDescription, string longDescription) {
		this.shortDescription.text = shortDescription;
	}	

	public void Use() {
		foreach(var e in actionEvents)
			e.Activate();

		FinishedEvent();
	}
}