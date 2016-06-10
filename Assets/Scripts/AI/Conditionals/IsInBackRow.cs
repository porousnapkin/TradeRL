using UnityEngine;
using System.Collections;

public class IsInBackRow : CombatAIConditional{
    public AICombatController controller;

    public bool Passes()
    {
        return !controller.GetCharacter().IsInMelee;
    }
}
