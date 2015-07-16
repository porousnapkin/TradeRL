using UnityEngine;

public class EndPlayerTurnEvent : StoryActionEvent {
	public PlayerController controller;

	public void Activate() {
		controller.EndTurn();
	}		
}