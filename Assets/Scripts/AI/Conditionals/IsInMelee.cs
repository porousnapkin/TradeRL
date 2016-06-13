using UnityEngine;
using System.Collections;

public class IsInMelee : CombatAIConditional{
    public AICombatController controller;

    protected override bool Check()
    {
        return controller.GetCharacter().IsInMelee;
    }
}
