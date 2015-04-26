using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class GridInputCollector : MonoBehaviour {
	public LayerMask layerMask;
	GridInputPosition activePoint;

	void Update() {
		SetupActivePoint();	
		CheckInput();
		CheckMouseOverPoint();
	}

	void SetupActivePoint() {
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, layerMask);
		var hitList = hits.ToList();
		hitList.Sort((h1, h2) => (int)((h2.distance - h1.distance) * 100));
		if(hitList.Count > 0)
			activePoint = hitList[0].collider.gameObject.GetComponent<GridInputPosition>();
	}

	void CheckInput() {
		if(Input.GetMouseButtonDown(0))
			MouseClicked();
	}

	void MouseClicked() {
		if(activePoint != null)
			PlayerController.Instance.ClickedOnPosition(activePoint.position);
	}

	void CheckMouseOverPoint() {
		if(activePoint != null)
			PlayerController.Instance.MouseOverPoint(activePoint.position);
	}
}