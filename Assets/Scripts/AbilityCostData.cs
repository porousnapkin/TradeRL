using System;
using UnityEngine;

public abstract class AbilityCostData : ScriptableObject {
	public abstract Cost Create(Character owner);
}