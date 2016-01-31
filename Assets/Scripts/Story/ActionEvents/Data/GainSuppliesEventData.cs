using UnityEngine;
using System.Collections;

public class GainSuppliesEventData : StoryActionEventData {
	public int numSupplies;

	public override StoryActionEvent Create() {
		var e = DesertContext.StrangeNew<GainSuppliesEvent>();
		e.supplies = numSupplies;
		return e;
	}
}
