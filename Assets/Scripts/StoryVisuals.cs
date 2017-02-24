using UnityEngine;
using UnityEngine.UI;

public class StoryVisuals : MonoBehaviour {
	public Text title;
	public Text description;
	public Transform storyActionParent;
	public event System.Action storyFinishedEvent = delegate{};

	public void Setup(string title, string description) {
		this.title.text = title;
		this.title.gameObject.SetActive(title != "");
		this.description.text = description;
	}

	public void AddAction(GameObject actionGO) {
		actionGO.transform.SetParent(storyActionParent);
	}

	public void Finished() {
		storyFinishedEvent();
		GameObject.Destroy(gameObject);	
	}

    public void AddSpecialCaseString(string key, string s)
    {
        title.text = title.text.Replace("[" + key + "]", s);
        description.text = description.text.Replace("[" + key + "]", s);
    }
}
