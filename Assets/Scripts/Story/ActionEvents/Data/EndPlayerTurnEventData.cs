using UnityEngine;

public class EndPlayerTurnEventData : StoryActionEventData {
	public override StoryActionEvent Create() {
		return StoryFactory.CreateEndPlayerTurnEvent();
	}
}