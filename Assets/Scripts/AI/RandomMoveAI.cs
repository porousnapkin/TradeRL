using UnityEngine;

public class RandomMoveAI : NPCAI {
	public AIController controller;

	public void RunTurn() {
		Vector2 endPosition = controller.character.WorldPosition + GetMoveAmount();
		controller.Move(endPosition);
		controller.EndTurn();
	}

	Vector2 GetMoveAmount() {
		Vector2 move = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));	
		if(move.magnitude <= 0.1f)
			return GetMoveAmount();

		return move;
	}
}