using UnityEngine;
using System.Collections;

public class GainSuppliesEventData : StoryActionEventData {
	public int numSupplies;

	public override StoryActionEvent Create() {
		return StoryFactory.CreateGainSuppliesEvent(numSupplies);
	}
}
