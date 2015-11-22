using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryVisuals : MonoBehaviour {
	public Text description;
	public Transform storyActionParent;
	public event System.Action storyFinishedEvent = delegate{};

	public void Setup(string description) {
		this.description.text = description;
	}

	public void AddAction(GameObject actionGO) {
		actionGO.transform.SetParent(storyActionParent);
	}

	public void Finished() {
		storyFinishedEvent();
		GameObject.Destroy(gameObject);	
	}
}
