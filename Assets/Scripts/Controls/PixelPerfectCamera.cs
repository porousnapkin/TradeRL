using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {
	public PlayerController playerController;
	public float textureSize = 64.0f;
	float unitsPerPixel;
	public float closenessPercent = 0.1f;
	
	void Start () {
		unitsPerPixel = 100;
		// Camera.main.orthographicSize = (Screen.height / 2f) / unitsPerPixel;
		Invoke ("ImmediateMoveToGoalPosition", 0.01f);
	}

	void ImmediateMoveToGoalPosition() {
		transform.position = PixelPerfectizePosition (GetGoalPosition ());
	}

	void Update() {
		var goalPosition = GetGoalPosition ();
		var outPosition = Vector3.Lerp(transform.position, new Vector3(goalPosition.x, goalPosition.y, transform.position.z), closenessPercent);

		transform.position = PixelPerfectizePosition (outPosition);
	}

	Vector3 GetGoalPosition() {
		Vector2 worldPos = playerController.playerCharacter.WorldPosition;
		var goalPosition = Grid.GetCharacterWorldPositionFromGridPositon((int)worldPos.x, (int)worldPos.y);
		return goalPosition;
	}

	Vector3 PixelPerfectizePosition(Vector3 pos) {
		return new Vector3(RoundToCloseness(pos.x, 0.01f), RoundToCloseness(pos.y, 0.01f), pos.z);
	}

	float RoundToCloseness(float input, float closeness) {
		return input - (input % closeness);
	}
}
