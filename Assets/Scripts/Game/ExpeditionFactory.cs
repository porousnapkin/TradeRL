using UnityEngine;
using System.Collections;

public class ExpeditionFactory {
	static Expedition activeExpedition = null;
	Expedition ActiveExpedition { get { return activeExpedition; } }

	public void BeginExpedition(Town destination) {
		activeExpedition = DesertContext.StrangeNew<Expedition>();

		activeExpedition.Begin(destination);
	}

	public void FinishExpedition() {
		if(activeExpedition != null)
			activeExpedition.Finish();
	}
}
