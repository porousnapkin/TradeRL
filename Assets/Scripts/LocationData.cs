using UnityEngine;

public class LocationData : ScriptableObject {
	public string locationName;
	public Sprite art;
	public StoryData firstStory;
    public bool randomlyPlace = true;
    public bool hasGuard = false;
    public TravelingStoryData guard;
}
