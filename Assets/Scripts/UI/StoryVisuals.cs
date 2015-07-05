using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryVisuals : MonoBehaviour {
	public Text description;
	public Transform storyActionParent;
	public GameObject storyActionPrefab;

	public void Setup(string description, List<GameObject> actions) {
		this.description.text = description;

		foreach(var go in actions)
			go.transform.SetParent(storyActionParent);
	}

	public void Finished() {
		GameObject.Destroy(gameObject);	
	}
}
