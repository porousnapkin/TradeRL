using UnityEngine;

public abstract class TravelingStoryAIRoutineData : ScriptableObject {
    public TravelingStorySpeed speed;
	public abstract TravelingStoryAIRoutine Create();
}