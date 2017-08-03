using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageRaycasterPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string defaultText = "";
    InputPopupHandler handler = new InputPopupHandler();

    void Awake()
    {
        handler.defaultText = defaultText;
        handler.Setup();
    }

    void OnDestroy()
    {
        handler.Destroy();
    }

    public int ReserveSpace()
    {
        return handler.ReserveSpace();
    }

    public void Record(string s, int fieldIndex = 0)
    {
        handler.Record(s, fieldIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        handler.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        handler.Hide();
    }
}
