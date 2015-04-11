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
	}

	void Update() {
		var goalPosition = playerController.CharacterGO.transform.position;
		var outPosition = Vector3.Lerp(transform.position, new Vector3(goalPosition.x, goalPosition.y, transform.position.z), closenessPercent);

		transform.position = new Vector3(RoundToCloseness(outPosition.x, 0.01f), RoundToCloseness(outPosition.y, 0.01f), outPosition.z);
		// transform.position = outPosition;
	}

	float RoundToCloseness(float input, float closeness) {
		return input - (input % closeness);
	}
}
