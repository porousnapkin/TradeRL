using UnityEngine;
using System.Collections.Generic;

public class AICharacterData : ScriptableObject {
	public string displayName = "Enemy";
	public Sprite visuals; 
	public int hp = 50;
	public List<AIActionData> actions;

	public Character Create(Faction faction, Vector2 position) {
		return AICharacterFactory.CreateAICharacter(this, faction, position);
	}
}