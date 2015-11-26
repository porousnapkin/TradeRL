using UnityEngine;
using System.Collections;

public class TravelingStoryVisuals : MonoBehaviour {
	public SpriteRenderer sprite;
	[HideInInspector]public TravelingStoryData data;
	[HideInInspector]public MapGraph mapGraph;
	[HideInInspector]public TurnManager turnManager;
	Vector2 position;
	public Vector2 WorldPosition { 
		get { return position; }
		set {
			mapGraph.RemoveEventAtLocation((int)position.x, (int)position.y, false);
			position = value;

			var occupant = mapGraph.GetPositionOccupant((int)position.x, (int)position.y);
			if(occupant != null)
				Activate(() => {});
			else
				mapGraph.SetEventForLocation((int)position.x, (int)position.y, (f) => Activate(f), false);
		}
	}
	
	void Start () {
		sprite.sprite = data.art;
		turnManager.TurnEndedEvent += HandleTurnEndedEvent;
	}

	void HandleTurnEndedEvent () {
		mapGraph.TravelingStoryVacatesPosition(WorldPosition);
		WorldPosition = GetMoveToPosition();
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);

		AnimationController.Move(gameObject, WorldPosition);
	}

	Vector2 GetMoveToPosition() {
		var offset = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		var newPosition = position + offset;

		if(!Grid.IsValidPosition((int)newPosition.x, (int)newPosition.y) ||
		   	mapGraph.GetTravelingStoryAtLocation(newPosition) != null)
			return GetMoveToPosition();
		return newPosition;
	}

	void Activate(System.Action finishedDelegate) {
		data.Activate(finishedDelegate);

		GameObject.Destroy(gameObject);
	}

	public void TeleportToWorldPosition() {
		transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)WorldPosition.x, (int)WorldPosition.y);
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
	}
}
