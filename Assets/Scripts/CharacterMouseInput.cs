using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CharacterMouseInput : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public Character owner;
    public event System.Action<Character> mouseOver = delegate { };
    public event System.Action<Character> mouseExit = delegate { };
    public event System.Action<Character> mouseDown = delegate { };

	public void OnPointerEnter(PointerEventData eventData) {
        mouseOver(owner);
	}

    public void OnPointerExit(PointerEventData eventData) {
        mouseExit(owner);
	}

	public void OnPointerDown(PointerEventData eventData) {
        mouseDown(owner);
	}
}
