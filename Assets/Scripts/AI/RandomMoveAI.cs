using UnityEngine;

public class RandomMoveAI : NPCAI {
	public AIController controller;
	public MapGraph mapGraph;

	public void RunTurn() {
		Vector2 endPosition = controller.character.WorldPosition + GetMoveAmount();
		if(mapGraph.IsPositionOccupied((int)endPosition.x, (int)endPosition.y))
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