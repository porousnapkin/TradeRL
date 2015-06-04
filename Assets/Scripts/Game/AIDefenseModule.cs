using UnityEngine;
using System.Collections.Generic;

public class AIDefenseModule : DefenseModule {
	public int defenseValue = 10;
	public int damageReduction = 0;

	public int GetDefenseValue() { return defenseValue; }
	public int ModifyIncomingDamage(int damage) { return Mathf.Max(0, damage - damageReduction); }
	public List<string> GetNotes(Character attacker) { return new List<string>(); }
}