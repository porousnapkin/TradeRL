using UnityEngine;
using System.Collections;

public enum LocationType {
	OneOffStory,
	ActiveStoryWithCooldown,
	ConstantStory,
}

public class LocationData : ScriptableObject {
	public string locationName;
	public string description;
	public Sprite art;
	public LocationType activationType;
	public int cooldownTurns;
	public StoryData firstStory;
	public StoryData secondStory;
}
