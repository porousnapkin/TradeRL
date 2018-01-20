using UnityEngine;
using System.Collections.Generic;

public class AbilityInitiativeModifier : AbilityModifier
{
	[Inject]public GlobalTextArea textArea {private get; set; }
	public int initiativeModifier = 2;
	public string initiativeSource = "Quick";
    public CombatController owner;
    public bool persist { private get; set; }
    CombatController.InitiativeModifier mod;

    public void PrepareActivation(List<Character> targets, System.Action callback)
    {
        mod = new CombatController.InitiativeModifier();
        mod.amount = initiativeModifier;
        mod.description = initiativeSource;
        mod.removeAtTurnEnd = !persist;

        targets.ForEach(t => t.controller.AddInitiativeModifier(mod));

        textArea.AddLine(GetInitiativeModifierString(initiativeModifier, owner.character, initiativeSource));
        callback();
    }

    public void BeforeActivation(List<Character> targets, System.Action callback) { callback(); }
    public void ActivationEnded(List<Character> targets, System.Action callback) { callback(); }

    public static string GetInitiativeModifierString(int initiativeModifier, Character affected, string initiativeSource)
    {
        string direction = "increased";
		if(initiativeModifier < 0)
			direction = "decreased";
		
		return affected.displayName + "'s initiative next round " + direction + 
			" by " + Mathf.Abs(initiativeModifier) + " from " + initiativeSource;       
    }
}

