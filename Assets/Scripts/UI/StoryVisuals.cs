using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryVisuals : MonoBehaviour {
	public Text title; //Do I want a title?
	public Text description;
	public Transform storyActionParent;
	public GameObject storyActionPrefab;

	public void Setup(string title, string description, List<StoryAction> actions) {
		this.title.text = title;
		this.description.text = description;

		foreach(var action in actions) 
			CreateAction(action);
	}

	void CreateAction(StoryAction action) {
		var actionGO = GameObject.Instantiate(storyActionPrefab) as GameObject;
		actionGO.transform.parent = storyActionParent;

		actionGO.GetComponent<StoryActionVisuals>().Setup(action);
	}
}

public class StoryAction {
	public float chanceSuccess = 0.5f;
	public float effortToSurpass = 4;
	public string shortDescription = "Flee";
	public string longDescription = "Attempt to escape the fight";

	public bool Attempt() {
		return Random.value < chanceSuccess;
	}

	public void UseEffort() {
		//Spend effort... 	
	}
}