using UnityEngine;
using UnityEngine.EventSystems;

public class GridInputPosition : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler {
	public Vector2 position;
	public GridInputCollectorView gridInputCollector; 

	public void OnPointerEnter(PointerEventData eventData) {
		gridInputCollector.SetActivePoint(this);
	}

	public void OnPointerDown(PointerEventData eventData) {
		gridInputCollector.PointClicked(this);
	}
}