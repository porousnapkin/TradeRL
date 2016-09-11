using UnityEngine;

public class DestinationDoober : MonoBehaviour {
	public Vector2 destinationPosition;	
	RectTransform t;
	RectTransform pT;

	void Start() {
		t = GetComponent<RectTransform>();
		pT = transform.parent.GetComponent<RectTransform>();

		GlobalEvents.LocationPickedEvent += LocationPicked;
	}

	void LocationPicked(Town t) {
		destinationPosition = t.worldPosition;
		gameObject.SetActive(true);
	}

	void Update() {
		var worldDest = Grid.GetBaseWorldPositionFromGridPosition((int)destinationPosition.x, (int)destinationPosition.y);	
		var curPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldDest) - pT.sizeDelta / 2f;

		float xExtent = (pT.rect.width/2) - (t.rect.width/2);
		float yExtent = (pT.rect.height/2) - (t.rect.height/2);
		if(curPos.x > xExtent)
			curPos.x = xExtent;
		if(curPos.x < -xExtent)
			curPos.x = -xExtent;
		if(curPos.y > yExtent)
			curPos.y = yExtent;
		if(curPos.y < -yExtent)
			curPos.y = -yExtent;

		t.anchoredPosition = curPos;
	}
}