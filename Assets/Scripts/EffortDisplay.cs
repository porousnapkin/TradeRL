using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

public class EffortDisplay : DesertView {
	public Text text;

	public void UpdateDisplay(Effort effort) {
        text.text = "";
        foreach(Effort.EffortType type in System.Enum.GetValues(typeof(Effort.EffortType)))
		    text.text += type + ": " + effort.GetEffort(type) + " / " + effort.GetMaxEffort(type) + "\n";
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
		display.UpdateDisplay(effort);
	}
	
	public override void OnRemove()
	{
		effort.EffortChangedEvent -= UpdateDisplay;
		effort.MaxEffortChangedEvent -= UpdateDisplay;
	}
}
