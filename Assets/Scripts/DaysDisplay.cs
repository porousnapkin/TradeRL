using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class DaysDisplay : DesertView {
	public Text text;
	
	public void UpdateDisplay(int days) {
		text.text = "Days: " + days;
	}
}

public class DaysDisplayMediator : Mediator {
	[Inject] public DaysDisplay display { private get; set; }
	[Inject] public GameDate date { private get; set; }

	int days = 0;

	public override void OnRegister ()
	{
		date.DaysPassedEvent += UpdateDisplay;
		UpdateDisplay(0);
	}
	
	void UpdateDisplay(int val) {
		days += val;
		display.UpdateDisplay(days);
	}
	
	public override void OnRemove()
	{
		date.DaysPassedEvent -= UpdateDisplay;
	}
}
