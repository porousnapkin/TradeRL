using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour {
	public Text text;	
	public Health health;

	void Start() {
		health.HealthChangedEvent += UpdateDisplay;
		health.MaxHealthChangedEvent += UpdateDisplay;
		UpdateDisplay(0);
	}

	void UpdateDisplay(int val) {
		text.text = "HP: " + health.Value + " / " + health.MaxValue;
	}
}