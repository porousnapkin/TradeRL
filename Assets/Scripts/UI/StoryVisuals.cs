using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryVisuals : MonoBehaviour {
	public Text description;
	public Transform storyActionParent;
	public GameObject storyActionPrefab;

	public void Setup(string description, List<StoryAction> actions) {
		this.description.text = description;

		foreach(var action in actions) 
			CreateAction(action);
	}

	void CreateAction(StoryAction action) {
		var actionGO = GameObject.Instantiate(storyActionPrefab) as GameObject;
		actionGO.transform.SetParent(storyActionParent);

		actionGO.GetComponent<StoryActionVisuals>().Setup(action);
	}
}
