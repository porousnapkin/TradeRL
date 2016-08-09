using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

//TODO: Everything about this is now wrong...
public class EffortDisplay : DesertView {
	public Text text;

	public void UpdateDisplay(int effort, int max) {
		text.text = "Effort: " + effort + " / " + max;
	}
}

public class EffortDisplayMediator : Mediator {
	[Inject] public EffortDisplay display { private get; set; }
	[Inject] public Effort effort { private set; get; }
	
	public override void OnRegister ()
	{
		effort.EffortChangedEvent += UpdateDisplay;
		effort.MaxEffortChangedEvent += UpdateDisplay;
		UpdateDisplay();
	}
	
	void UpdateDisplay () 
	{
        //TODO: Reimplement
		//display.UpdateDisplay(effort.Value, effort.MaxValue);
	}
	
	public override void OnRemove()
	{
		effort.EffortChangedEvent -= UpdateDisplay;
		effort.MaxEffortChangedEvent -= UpdateDisplay;
	}
}
