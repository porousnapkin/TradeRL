using UnityEngine;
using System.Collections.Generic;

public class CombatEncounterData : ScriptableObject {
	public List<AICharacterData> characters;	

	public void Create() {
		EncounterFactory.CreateEncounter(characters);
	}
}