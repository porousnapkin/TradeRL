using UnityEngine;
using System.Collections.Generic;

public class EncounterFactory {
	const int minRootDistance = 4;
	const int maxRootDistance = 6;
	const int maxCharacterDistanceFromRoot = 4;
	public static Character player;
	public static MapGraph mapGraph;

	public static void CreateEncounter(List<AICharacterData> characters) {
		var rootEncounterPosition = GetRootEncounterPosition(); 

		foreach(var c in characters)
			CreateCharacter(c, rootEncounterPosition, Faction.Enemy);
	}

	static Vector2 GetRootEncounterPosition() {
		Vector2 offset = GetEncounterPositionOffset();

		var position = player.WorldPosition + offset;	
		if(Grid.IsValidPosition((int)position.x, (int)position.y))
			return position;
		else
			return GetRootEncounterPosition();
	}

	static Vector2 GetEncounterPositionOffset() {
		//Randomly constrain on vertical or horizontal axis.
		if(Random.value > 0.5f)
			return new Vector2(Random.Range(minRootDistance, maxRootDistance), Random.Range(0, minRootDistance));
		else
			return new Vector2(Random.Range(0, minRootDistance), Random.Range(minRootDistance, maxRootDistance));
	}

	static void CreateCharacter(AICharacterData characterData, Vector2 rootEnounterPosition, Faction f) {
		var characterPosition = GetCharacterPosition(rootEnounterPosition);
		Debug.Log("Root pos " + rootEnounterPosition + ", spawnPos " + characterPosition);
		var character = characterData.Create(f, characterPosition);	
	}

	static Vector2 GetCharacterPosition(Vector2 rootEncounterPosition) {
		if(!mapGraph.IsPositionOccupied((int)rootEncounterPosition.x, (int)rootEncounterPosition.y))
			return rootEncounterPosition;

		var offset = new Vector2(Random.Range(0, maxCharacterDistanceFromRoot), Random.Range(0, maxCharacterDistanceFromRoot));
		var position = rootEncounterPosition + offset;

		if(!mapGraph.IsPositionOccupied((int)position.x, (int)position.y))
			return position;
		else
			return GetCharacterPosition(rootEncounterPosition);
	}
}