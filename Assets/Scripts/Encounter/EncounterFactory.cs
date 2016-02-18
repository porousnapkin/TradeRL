using UnityEngine;
using System.Collections.Generic;

public interface EncounterFactory {
	void CreateEncounter(CombatEncounterData data);
}

public class StubEncounterFactory : EncounterFactory {
	[Inject(Character.PLAYER)] public Character playerCharacter { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set;}

	public void CreateEncounter(CombatEncounterData data) {
		int damage = data.stubBaseDamage + Random.Range(0, data.stubDamageRange);
		playerCharacter.health.Damage(damage);
		textArea.AddLine("STUB COMBAT: Take " + damage + " damage.");
		//TODO: Send message to text area about damage...
	}
}

public class CombatEncounterFactory : EncounterFactory {
	const int minRootDistance = 4;
	const int maxRootDistance = 6;
	const int maxCharacterDistanceFromRoot = 4;

	[Inject] public AICharacterFactory aiCharacterFactory { private get; set; }
	[Inject(Character.PLAYER)] public Character player { private get; set; }
	[Inject] public CombatGraph combatGraph { private get; set; }
	[Inject] public CombatFactory combatFactory { private get; set; }

	public void CreateEncounter(CombatEncounterData data) {
		combatFactory.CreateCombat();
	}

	Vector2 GetRootEncounterPosition() {
		Vector2 offset = GetEncounterPositionOffset();

		var position = player.Position + offset;	
		if(Grid.IsValidPosition((int)position.x, (int)position.y))
			return position;
		else
			return GetRootEncounterPosition();
	}

	Vector2 GetEncounterPositionOffset() {
		//Randomly constrain on vertical or horizontal axis.
		if(Random.value > 0.5f)
			return new Vector2(Random.Range(minRootDistance, maxRootDistance), Random.Range(0, minRootDistance));
		else
			return new Vector2(Random.Range(0, minRootDistance), Random.Range(minRootDistance, maxRootDistance));
	}

	void CreateCharacter(AICharacterData characterData, Vector2 rootEnounterPosition, Faction f) {
		var characterPosition = GetCharacterPosition(rootEnounterPosition);
		aiCharacterFactory.CreateAICharacter(characterData, f, characterPosition);
	}

	Vector2 GetCharacterPosition(Vector2 rootEncounterPosition) {
		if(!combatGraph.IsPositionOccupied((int)rootEncounterPosition.x, (int)rootEncounterPosition.y))
			return rootEncounterPosition;

		var offset = new Vector2(Random.Range(0, maxCharacterDistanceFromRoot), Random.Range(0, maxCharacterDistanceFromRoot));
		var position = rootEncounterPosition + offset;

		if(!combatGraph.IsPositionOccupied((int)position.x, (int)position.y))
			return position;
		else
			return GetCharacterPosition(rootEncounterPosition);
	}
}