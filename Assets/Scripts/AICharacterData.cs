using UnityEngine;

public class AICharacterData : ScriptableObject {
	public string displayName = "Enemy";
	public Sprite visuals; 
	public int hp = 50;
	public int minDamage = 10;
	public int maxDamage = 12;
	public int damageReduction = 0;
    public int initiative = 10;
    public int encounterPickerWeight = 2;

    public enum PositionPreference
    {
        PrefersFront,
        PrefersBack,
        RandomPosition
    }
    public PositionPreference positionPreference;
    public CombatAIData combatAI;
}