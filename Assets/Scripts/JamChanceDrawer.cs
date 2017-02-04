using UnityEngine;

public class JamChanceDrawer: MonoBehaviour
{
    public Item item;
    UIImageRaycasterPopup popup;
    int popupSpace;

    void Start()
    {
        SetupButtonDrawer();
        SetupPopup();
    }

    void OnDestroy()
    {
        item.jamChecksChanged -= RecordJamChecks;
        item.itemJammedEvent -= RecordJamChecks;
    }

    void SetupPopup()
    {
        popup = GetComponent<UIImageRaycasterPopup>();
        popupSpace = popup.ReserveSpace();

        RecordJamChecks();
        item.jamChecksChanged += RecordJamChecks;
        item.itemJammedEvent += RecordJamChecks;
    }

    void RecordJamChecks()
    {
        if(item.IsJammed())
            popup.Record("JAMMED", popupSpace);
        else
            popup.Record("Jam checks: " + (item.GetRemainingJamSaves() + 1) + "/" + (item.GetTotalJamSaves() + 1), popupSpace);
    }

    void SetupButtonDrawer()
    {
        var text = GetComponentInChildren<MultiWrittenTextField>();
        if(text == null)
            GameObject.Destroy(this);

        var fieldIndex = text.ReserveSpace();
        text.Record("Jam: " + Mathf.RoundToInt(item.jamChance * 100).ToString() + "%", fieldIndex);
    }
}

