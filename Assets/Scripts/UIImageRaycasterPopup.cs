using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageRaycasterPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.ShowPopup(description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.DoneWithPopup();
    }
}
