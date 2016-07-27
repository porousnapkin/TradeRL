using UnityEngine;
using System.Collections.Generic;

public interface EncounterFactory {
	void CreateEncounter(CombatEncounterData data);
}

public class StubEncounterFactory : EncounterFactory {
	//TODO: Fix this if we still want to support the stub factory...
	//[Inject(Character.PLAYER)] public Character playerCharacter { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set;}

	public void CreateEncounter(CombatEncounterData data) {
		int damage = data.stubBaseDamage + Random.Range(0, data.stubDamageRange);
		//playerCharacter.health.Damage(damage);
		textArea.AddLine("STUB COMBAT: Take " + damage + " damage.");
		//TODO: Send message to text area about damage...
	}
}

public class CombatEncounterFactory : EncounterFactory {
	const int minRootDistance = 4;
	const int maxRootDistance = 6;
	const int maxCharacterDistanceFromRoot = 4;

	[Inject] public AICharacterFactory aiCharacterFactory { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }

	public void CreateEncounter(CombatEncounterData data) {
		combatFactory.CreateCombat();
	}

	Vector2 GetEncounterPositionOffset() {
		//Randomly constrain on vertical or horizontal axis.
		if(Random.value > 0.5f)
			return new Vector2(Random.Range(minRootDistance, maxRootDistance), Random.Range(0, minRootDistance));
		else
			return new Vector2(Random.Range(0, minRootDistance), Random.Range(minRootDistance, maxRootDistance));
	}
}