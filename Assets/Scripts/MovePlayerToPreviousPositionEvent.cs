using UnityEngine;

public class MovePlayerToPreviousPositionEvent : StoryActionEvent {
	[Inject] public MapPlayerController controller { private get; set; }

	public void Activate() {
		controller.MoveToPreviousPosition();
	}		
}