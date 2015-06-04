using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GlobalTextArea : MonoBehaviour {
	static GlobalTextArea instance = null;
	public static GlobalTextArea Instance { get { return instance; }}
	public Text text;

	void Awake() { instance = this; }

	public void AddLine(string lineToAdd) {
		text.text += "\n" + lineToAdd;
	}

	public string CreateNotes(List<string> notes) {
		if(notes.Count == 0)
			return "";

		string noteString = "(";	
		for(int i = 0; i < notes.Count; i++) {
			noteString += notes[i];
			if(i != notes.Count - 1)
				noteString += ", ";
		}

		noteString += ")";

		return noteString;
	}

	public void AddDamageLineWithChanceToHit(Character attacker, Character defender, string presentTenseVerb, int damage, string notes = "") {
		AddLine(CreateAttackLineHeader(attacker, defender, presentTenseVerb) + " " + CreateChanceToHit(attacker, defender) + " " + 
			CreateDamageString(damage) + ". " + notes);
	}

	public void AddMissLineWithChanceToHit(Character attacker, Character defender, string presentTenseVerb, string notes = "") {
		AddLine(CreateAttackLineHeader(attacker, defender, presentTenseVerb) + " " + CreateChanceToHit(attacker, defender) + ". " + notes);	
	}

	public void AddDamageLine(Character attacker, Character defender, string presentTenseVerb, int damage, string notes = "") {
		AddLine(CreateAttackLineHeader(attacker, defender, presentTenseVerb) + " " + CreateDamageString(damage) + ". " + notes);
	}

	string CreateAttackLineHeader(Character attacker, Character defender, string presentTenseVerb) {
		return attacker.displayName + " " + presentTenseVerb + " " + defender.displayName;
	}

	string CreateChanceToHit(Character attacker, Character defender) {
		var chanceToHit = CombatModule.CalculateChanceToHit(attacker, defender);
		return "(" + chanceToHit + "%)";
	}

	string CreateDamageString(int damage) {
		return "for <color=Red>" + damage + "</color> damage";
	}

	public void AddDeathLine(Character dying) {
		AddLine(dying.displayName + " dies...");
	}
}