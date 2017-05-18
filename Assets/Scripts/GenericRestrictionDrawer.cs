using UnityEngine;

public class GenericRestrictionDrawer : MonoBehaviour
{
    public Restriction restriction { private get; set; }
    public string text { private get; set; }
    UIImageRaycasterPopup popup;
    int popupSpace;

    void Start()
    {
        popup = GetComponent<UIImageRaycasterPopup>();
        popupSpace = popup.ReserveSpace();
    }

    void Update()
    {
        if (restriction == null || restriction.CanUse())
            popup.Record("", popupSpace);
        else
            popup.Record("<color=red>" + text + "</color>", popupSpace);
    }
}
