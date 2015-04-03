using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour{
	static PlayerController instance;
	public static PlayerController Instance { get { return instance; }}
	void Awake() { instance = this; }

	public MapCreator mapCreator;
	DesertPathfinder pathfinder;
	Vector2 curPosition = new Vector2(50, 50);
	public GameObject pathPrefab;
	List<GameObject> pathObjects;
	const int size = 200;
	public GameObject characterPrefab;
	GameObject characterGO;
	bool isPathing = false;
	public GameObject CharacterGO { get { return characterGO; }}
	Vector2 lastDestination;
	public float travelTime = 0.25f;
	public HiddenGrid hiddenGrid;

	void Start() {
		pathfinder = mapCreator.Pathfinder;
		characterGO = GameObject.Instantiate(characterPrefab) as GameObject;
		characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)curPosition.x, (int)curPosition.y);
		hiddenGrid.SetPosition(curPosition);

		pathObjects = new List<GameObject>();
		for(int i = 0; i < size; i++) {
			var pathGO = GameObject.Instantiate(pathPrefab) as GameObject;
			pathObjects.Add(pathGO);
			pathGO.SetActive(false);
			pathGO.transform.parent = transform;
		}
	}

	public void MouseOverPoint(Vector2 destination) {
		lastDestination = destination;
		if(!isPathing)
			DrawPathToPosition(destination);	
	}

	void DrawPathToPosition(Vector2 destination) {
		var path = pathfinder.SearchForPathOnMainMap(curPosition, destination);
		for(int i = 1; i < path.Count; i++) {
			pathObjects[i].transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)path[i].x, (int)path[i].y);
			pathObjects[i].SetActive(true);
		}
		for(int i = path.Count; i < size; i++) 
			pathObjects[i].SetActive(false);

	}

	public void ClickedOnPosition(Vector2 destination) {
		if(!isPathing)
			PathToPosition(destination);
		else {
			LeanTween.cancel(characterGO);
			StopAllCoroutines();
			isPathing = false;
			characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)curPosition.x, (int)curPosition.y);
			DrawPathToPosition(lastDestination);
		}
	}

	void PathToPosition(Vector2 destination) {
		var path = pathfinder.SearchForPathOnMainMap(curPosition, destination);
		isPathing = true;

		StartCoroutine(PathCoroutine(path));
	}

	IEnumerator PathCoroutine(List<Vector2> path) {
		if(path.Count > 0)
			path.RemoveAt(0);
		foreach(var position in path) {
			curPosition = position;
			hiddenGrid.SetPosition(curPosition);
			LeanTween.move(characterGO, Grid.GetCharacterWorldPositionFromGridPositon((int)position.x, (int)position.y), travelTime)
				.setEase(LeanTweenType.easeOutQuad);
			yield return new WaitForSeconds(travelTime);
		}

		isPathing = false;
		DrawPathToPosition(lastDestination);
	}
}