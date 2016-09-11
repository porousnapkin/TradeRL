using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class StoryData : ScriptableObject {
	public string title;
	public string description;
	public List<StoryActionData> actions;
}