using UnityEngine;
using System.Collections.Generic;

public class GridHighlighter : MonoBehaviour {
	public GameObject pathPrefab;
	List<GameObject> pathObjects;
	public GameObject mouseOver;
	public bool isCombat = false;
	const int pathObjectsSize = 200;
	int curIndex = 0;

	void Awake() {
		pathObjects = new List<GameObject>();
		for(int i = 0; i < pathObjectsSize; i++) {
			var pathGO = GameObject.Instantiate(pathPrefab) as GameObject;
			pathObjects.Add(pathGO);
			pathGO.SetActive(false);
			pathGO.transform.parent = transform;
		}
	}

	public void MoveMouseOverImage(Vector2 position) {
		mouseOver.transform.position = GetDisplayPosition(position);
	}

	Vector3 GetDisplayPosition(Vector2 position) {
		if(isCombat)
			return Grid.GetCharacterCombatPosition((int)position.x, (int)position.y);
		else
			return Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y);
	}

	public void DrawPath(List<Vector2> path) {
		if(pathObjects[0] == null)
			return;
			
		curIndex = 0;
		for(int i = 1; i < path.Count; i++)
			DisplayPosition(path[i]);
		HideRemaining();
	}

	void DisplayPosition(Vector2 position) {
		pathObjects [curIndex].transform.position = GetDisplayPosition (position);
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

	public void DrawRangeFromPoint(Vector2 point, int minRange, int maxRange) {
		curIndex = 0;
		for(int x = -maxRange; x <= maxRange; x++) {
			for(int y = -maxRange; y <= maxRange; y++) {
				if(x < minRange && x > -minRange && y < minRange && y > -minRange)
					continue;

				Vector2 highlightPoint = point + new Vector2(x, y);
				if(Grid.IsValidPosition((int)highlightPoint.x, (int)highlightPoint.y)) 
					DisplayPosition(highlightPoint);
			}
		}

		HideRemaining();
	}
}