using UnityEngine;
using System.Collections.Generic;

public class AICharacterData : ScriptableObject {
	public string displayName = "Enemy";
	public Sprite visuals; 
	public int hp = 50;
	public int attack = 10;
	public int minDamage = 10;
	public int maxDamage = 12;
	public int defense = 10;
	public int damageReduction = 0;

	public List<AIActionData> actions;
}