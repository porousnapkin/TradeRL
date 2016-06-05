using UnityEngine;

public abstract class AIActionData : ScriptableObject {
	public abstract AIAction Create(AICombatController controller);
}

