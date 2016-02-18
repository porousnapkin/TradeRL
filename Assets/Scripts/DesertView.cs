using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class DesertView : View {
	protected override void Awake() {
		base.Awake();

		if(!registeredWithContext)
			AddViewToContext(this, true, PrefabGetter.contextView);
	}
}
