using UnityEngine;
using System.Collections;

public abstract class CombatAIConditionalData : ScriptableObject {
    public abstract CombatAIConditional Create(AICombatController controller);	
}
