using UnityEngine;
using System.Collections.Generic;

public interface EncounterFactory {
	void CreateEncounter(CombatEncounterData data);
}

public class CombatEncounterFactory : EncounterFactory {
	const int minRootDistance = 4;
	const int maxRootDistance = 6;
	const int maxCharacterDistanceFromRoot = 4;

	[Inject] public AICharacterFactory aiCharacterFactory { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }

	public void CreateEncounter(CombatEncounterData data) {
        combatFactory.CreateCombat(data);
	}
}