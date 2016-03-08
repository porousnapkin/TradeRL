using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHelper : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public event System.Action pointerDownEvent = delegate{};
	public event System.Action pointerUpEvent = delegate{};

	public void OnPointerDown(PointerEventData handler) {
		pointerDownEvent();
	}

	public void OnPointerUp(PointerEventData handler) {
		pointerUpEvent();
	}
}