using UnityEngine;
using System.Collections.Generic;

public class StoryVisualsTester : MonoBehaviour {
	void Start() {
		var sv = GetComponent<StoryVisuals>();

		var actions = new List<StoryAction>();
		actions.Add(CreateTempAction("Stealth", "Evade them"));
		actions.Add(CreateTempAction("Prepare", "Set up to kill them"));

		sv.Setup("You spot a pack of feral dogs.", actions);
	}

	StoryAction CreateTempAction(string shortDesc, string longDesc) {
		var sa = new StoryAction();	
		sa.shortDescription = shortDesc;
		sa.longDescription = longDesc;
		return sa;
	}
}