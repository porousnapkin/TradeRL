using UnityEngine;
using System.Collections.Generic;

public class GridHighlighter : MonoBehaviour {
	static GridHighlighter instance = null;
	public static GridHighlighter Instance { get { return instance; } }

	public GameObject pathPrefab;
	List<GameObject> pathObjects;
	const int pathObjectsSize = 200;
	int curIndex = 0;

	void Awake() {
		instance = this;
		pathObjects = new List<GameObject>();
		for(int i = 0; i < pathObjectsSize; i++) {
			var pathGO = GameObject.Instantiate(pathPrefab) as GameObject;
			pathObjects.Add(pathGO);
			pathGO.SetActive(false);
			pathGO.transform.parent = transform;
		}
	}

	public void DrawPath(List<Vector2> path) {
		curIndex = 0;
		for(int i = 1; i < path.Count; i++)
			DisplayPosition(path[i]);
		HideRemaining();
	}

	void DisplayPosition(Vector2 position) {
		pathObjects[curIndex].transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
		pathObjects[curIndex].SetActive(true);
		curIndex++;
	}

	public void HideHighlights() {
		curIndex = 0;
		HideRemaining();
	}

	void HideRemaining() {
		for(int i = curIndex; i < pathObjectsSize; i++)
			pathObjects[i].SetActive(false);
	}

	public void DrawRangeFromPoint(Vector2 point, int range) {
		curIndex = 0;
		for(int x = -range; x <= range; x++) {
			for(int y = -range; y <= range; y++) {
				if(x == 0 && y == 0)
					continue;

				Vector2 highlightPoint = point + new Vector2(x, y);
				if(Grid.IsValidPosition((int)highlightPoint.x, (int)highlightPoint.y)) 
					DisplayPosition(highlightPoint);
			}
		}

		HideRemaining();
	}
}