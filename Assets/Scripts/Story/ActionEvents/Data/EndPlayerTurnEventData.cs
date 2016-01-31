using UnityEngine;

public class EndPlayerTurnEventData : StoryActionEventData {
	public override StoryActionEvent Create() {
		return DesertContext.StrangeNew<EndPlayerTurnEvent>();
	}
}