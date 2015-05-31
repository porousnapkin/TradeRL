using UnityEngine;
using UnityEngine.UI;

public class EffortDisplay : MonoBehaviour {
	public Text text;	
	public Effort effort;

	public void SetEffort(Effort effort) {
		this.effort = effort;
		effort.EffortChangedEvent += UpdateDisplay;
		effort.MaxEffortChangedEvent += UpdateDisplay;
		UpdateDisplay(0);
	}

	void UpdateDisplay(int val) {
		text.text = "Effort: " + effort.Value + " / " + effort.MaxValue;
	}
}