using System.Collections.Generic;
using UnityEngine;

public class TravelingStoryData : ScriptableObject {
	public enum StepInAction {
		BeginStory,
		Combat,
        RandomEncounter
	}
    public bool use = true;
	public StepInAction stepInAction;
	public StoryData story;
	public CombatEncounterData combatData;
    public EncounterDifficulty difficulty;
    public List<EncounterFaction> encounterFactions= new List<EncounterFaction>();
	public Sprite art;
	public string dataName;
	public string description;
	public string spawnMessage = "Stuff";
	public TravelingStoryAIData ai;
    public int stealthRating = 4;
    public float rarityDiscardChance = 1.0f;
    public AIAbilityData ambushAbility;
    public int encounterCost = 5;
}
