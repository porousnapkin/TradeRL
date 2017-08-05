using UnityEngine;

public enum LocationType {
	OneOffStory,
	ActiveStoryWithCooldown,
	ConstantStory,
}

public class LocationData : ScriptableObject {
	public string locationName;
	public Sprite art;
	public LocationType activationType;
	public int cooldownTurns;
	public StoryData firstStory;
	public StoryData secondStory;
    public bool randomlyPlace = true;
    public bool hasGuard = false;
    public TravelingStoryData guard;
}
