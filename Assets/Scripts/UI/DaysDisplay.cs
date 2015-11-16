using UnityEngine;
using UnityEngine.UI;

public class DaysDisplay : MonoBehaviour {
	public Text text;
	public GameDate date;
	int days = 0;
	
	public void SetGameDate(GameDate date) {
		this.date = date;
		date.DaysPassedEvent += UpdateDisplay;
		UpdateDisplay(0);
	}
	
	void UpdateDisplay(int val) {
		days += val;
		text.text = "Days: " + days;
	}
}