using UnityEngine;
using System.Collections.Generic;

public class MapCreator : MonoBehaviour {
	public int width = 100;
	public int height = 100;
	public float tileWidth = 1;
	public float tileHeight = 0.5f;
	public List<GameObject> prefabs;

	void Start () {
		for(int x = 0; x < width; x++) {
			for(int y = 0; y < height; y++) {
				GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)], new Vector3(x * (tileWidth / 2) + y * (tileWidth / 2), -x * (tileHeight / 2) + y * (tileHeight / 2), 0), Quaternion.identity);
			}
		}
	}
}
