using UnityEngine;
using System.Collections;

public class PixelPerfectCamera : MonoBehaviour {

	public float textureSize = 64.0f;
	
	float unitsPerPixel;
	
	// Use this for initialization
	
	void Start () {
		
		unitsPerPixel = 1f / textureSize;
		
		Camera.main.orthographicSize = (Screen.height / 2f) * unitsPerPixel;
		
	}
}
