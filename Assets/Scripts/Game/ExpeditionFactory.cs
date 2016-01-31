using UnityEngine;
using System.Collections;

public class ExpeditionFactory {
	[Inject(Character.PLAYER)] public Character playerCharacter { private set; get; }

	static Expedition activeExpedition = null;
	public Expedition ActiveExpedition { get { return activeExpedition; } }

	public void BeginExpedition(Town destination) {
		activeExpedition = DesertContext.StrangeNew<Expedition>();

		activeExpedition.playerCharacter = playerCharacter;
		activeExpedition.Begin(destination);
	}

	public void FinishExpedition() {
		if(activeExpedition != null)
			activeExpedition.Finish();
	}
}
