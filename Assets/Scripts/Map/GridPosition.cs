using UnityEngine;
using UnityEngine.EventSystems;

public class GridPosition : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {
	public Vector2 position;

	public void OnPointerEnter(PointerEventData eventData) {
		PlayerController.Instance.MouseOverPoint(position);
	}

	public void OnPointerClick(PointerEventData eventData) {
		PlayerController.Instance.ClickedOnPosition(position);
	}
}