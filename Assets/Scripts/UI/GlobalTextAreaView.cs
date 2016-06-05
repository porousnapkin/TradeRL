using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class GlobalTextAreaView : DesertView {
	static GlobalTextAreaView instance = null;
	public static GlobalTextAreaView Instance { get { return instance; }}
	public Text text;

	protected override void Awake() { 
		instance = this; 

		base.Awake();
	}

	public void AddLine(string lineToAdd) {
		text.text += "\n" + lineToAdd;
	}
}

public class GlobalTextAreaMediator : Mediator {
	[Inject] public GlobalTextAreaView view { private get; set; }
	[Inject] public GlobalTextArea model { private get; set; }

    public override void OnRegister()
    {
    	model.addLineEvent += LineAdded;
    }

    void LineAdded(string line) {
    	view.AddLine(line);
    }

    public override void OnRemove()
    {
    	model.addLineEvent -= LineAdded;
    }
}


public class GlobalTextArea {
	public event System.Action<string> addLineEvent = delegate{};

	public void AddLine(string line) {
		addLineEvent(line);
	}

	public static string CreateNotes(List<DamageModifierData> damageMods) {
		if(damageMods.Count == 0)
			return "";

		string noteString = "(";	
		for(int i = 0; i < damageMods.Count; i++) {
            noteString += damageMods[i].damageModSource;
            if (damageMods[i].damageMod > 0)
                noteString += " +" + damageMods[i].damageMod;
            else
                noteString += damageMods[i].damageMod;
			if(i != damageMods.Count - 1)
				noteString += ", ";
		}

		noteString += ")";

		return noteString;
	}

	public void AddDamageLine(Character attacker, Character defender, string presentTenseVerb, int damage, string notes = "") {
		AddLine(CreateAttackLineHeader(attacker, defender, presentTenseVerb) + " " + CreateDamageString(damage) + ". " + notes);
	}

	string CreateAttackLineHeader(Character attacker, Character defender, string presentTenseVerb) {
		return attacker.displayName + " " + presentTenseVerb + " " + defender.displayName;
	}

	string CreateDamageString(int damage) {
		return "for <color=Red>" + damage + "</color> damage";
	}

	public void AddDeathLine(Character dying) {
		AddLine(dying.displayName + " dies...");
	}
}
