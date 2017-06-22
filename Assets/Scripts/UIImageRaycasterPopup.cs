using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageRaycasterPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string defaultText = "";
    MultiWrittenString multiString = new MultiWrittenString("\n\n");
    bool active = false;

    void Awake()
    {
        multiString.stringAltered += UpdateDescription;

        if(defaultText != "")
        {
            ReserveSpace();
            Record(defaultText, 0);
        }
        else
            UpdateDescription();
    }

    void OnDestroy()
    {
        multiString.stringAltered -= UpdateDescription;
        if (active)
        {
            SingletonPopup.Instance.DoneWithPopup();
            active = false;
        }
    }

    private void UpdateDescription()
    {
        if (active)
            SingletonPopup.Instance.UpdateDescription(multiString.Write());
    }

    public int ReserveSpace()
    {
        return multiString.ReserveSpace();
    }

    public void Record(string s, int fieldIndex=0)
    {
        multiString.Record(s, fieldIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (active)
            return;
        var description = multiString.Write();
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.ShowPopup(description);
        active = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!active)
            return;
        var description = multiString.Write();
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.DoneWithPopup();
        active = false;
    }
}
