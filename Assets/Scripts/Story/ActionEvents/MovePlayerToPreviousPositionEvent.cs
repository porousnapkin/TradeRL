using UnityEngine;

public class MovePlayerToPreviousPositionEvent : StoryActionEvent {
	public PlayerController playerController;

	public void Activate() {
		playerController.ForceMoveToPreviousPosition();
	}		
}