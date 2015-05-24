using UnityEngine;
using UnityEngine.UI;

public class GlobalTextArea : MonoBehaviour {
	static GlobalTextArea instance = null;
	public static GlobalTextArea Instance { get { return instance; }}
	public Text text;

	void Awake() { instance = this; }

	public void AddLine(string lineToAdd) {
		text.text += "\n" + lineToAdd;
	}
}