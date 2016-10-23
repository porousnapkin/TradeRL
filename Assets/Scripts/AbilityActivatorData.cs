using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityActivatorData : ScriptableObject {
	public abstract AbilityActivator Create(CombatController owner);
}

