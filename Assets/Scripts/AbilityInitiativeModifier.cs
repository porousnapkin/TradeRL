using UnityEngine;
using System.Collections.Generic;

public class AbilityInitiativeModifier : AbilityModifier
{
	[Inject]public GlobalTextArea textArea {private get; set; }
	public int initiativeModifier = 2;
	public string initiativeSource = "Quick";
    public CombatController owner;
    public bool persistNewInitiative { private get; set; }

	public void BeforeActivation(List<Character> targets, System.Action callback) 
	{
		var curInitiative = owner.GetInitiative(1);
		owner.SetInitiative(1, curInitiative + initiativeModifier, persistNewInitiative);

        textArea.AddLine(GetInitiativeModifierString(initiativeModifier, owner.character, initiativeSource));
        callback();
	}

    public static string GetInitiativeModifierString(int initiativeModifier, Character affected, string initiativeSource)
    {
        string direction = "increased";
		if(initiativeModifier < 0)
			direction = "decreased";
		
		return affected.displayName + "'s initiative next round " + direction + 
			" by " + Mathf.Abs(initiativeModifier) + " from " + initiativeSource;       
    }

	public void ActivationEnded(List<Character> targets, System.Action callback) { callback(); }
}

