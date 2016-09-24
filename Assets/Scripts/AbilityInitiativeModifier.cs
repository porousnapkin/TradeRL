using UnityEngine;
using System.Collections.Generic;

public class AbilityInitiativeModifier : AbilityModifier
{
	[Inject]public GlobalTextArea textArea {private get; set; }
	public int initiativeModifier = 2;
	public string initiativeSource = "Quick";

	public void BeforeActivation(CombatController owner, List<Character> targets) 
	{
		var curInitiative = owner.GetInitiative(1);
		owner.SetInitiative(1, curInitiative + initiativeModifier);

        textArea.AddLine(GetInitiativeModifierString(initiativeModifier, owner.character, initiativeSource));
	}

    public static string GetInitiativeModifierString(int initiativeModifier, Character affected, string initiativeSource)
    {
        string direction = "increased";
		if(initiativeModifier < 0)
			direction = "decreased";
		
		return affected.displayName + "'s initiative next round " + direction + 
			" by " + Mathf.Abs(initiativeModifier) + " from " + initiativeSource;       
    }

	public void ActivationEnded(CombatController owner, List<Character> targets) {}
}

