using UnityEngine;
using System.Collections.Generic;

public class CombatEncounterData : ScriptableObject {
	public int stubBaseDamage = 3;
	public int stubDamageRange = 2;
	public List<AICharacterData> characters;
}