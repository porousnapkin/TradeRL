using UnityEngine;
using UnityEngine.EventSystems;

public class UIImageRaycasterPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MultiWrittenString multiString;
    bool active = false;

    void Awake()
    {
        multiString = new MultiWrittenString("\n\n");
        multiString.stringAltered += UpdateDescription;
    }

    void OnDestroy()
    {
        multiString.stringAltered -= UpdateDescription;
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

    public void Record(string s, int fieldIndex)
    {
        multiString.Record(s, fieldIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var description = multiString.Write();
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.ShowPopup(description);
        active = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var description = multiString.Write();
        if (description == "" || description == null)
            return;
        SingletonPopup.Instance.DoneWithPopup();
        active = false;
    }
}
