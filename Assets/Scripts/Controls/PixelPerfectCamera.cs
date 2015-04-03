using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {
	public PlayerController playerController;
	public float textureSize = 64.0f;
	float unitsPerPixel;
	public float closenessPercent = 0.1f;
	
	void Start () {
		//TODO: Need to move pixel perfectly...
		// unitsPerPixel = 1f / textureSize;
		// Camera.main.orthographicSize = (Screen.height / 2f) * unitsPerPixel;
	}

	void Update() {
		var goalPosition = playerController.CharacterGO.transform.position;
		var outPosition = Vector3.Lerp(transform.position, new Vector3(goalPosition.x, goalPosition.y, transform.position.z), closenessPercent);
		transform.position = new Vector3(Mathf.Round(outPosition.x * 100) / 100, Mathf.Round(outPosition.y * 100) / 100, outPosition.z);
	}
}
