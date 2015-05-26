using UnityEngine;
using System.Collections.Generic;

public class AICharacterData : ScriptableObject {
	public string displayName = "Enemy";
	public Sprite visuals; 
	public int hp = 50;
	public List<AIActionData> actions;

	public Character Create(Faction faction) {
		return AICharacterFactory.CreateAICharacter(this, faction, new Vector2(45, 45));
	}
}