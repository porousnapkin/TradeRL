using UnityEngine;

public class MovePlayerToPreviousPositionEventData : StoryActionEventData {
	public override StoryActionEvent Create() {
		return DesertContext.StrangeNew<MovePlayerToPreviousPositionEvent>();
	}
}