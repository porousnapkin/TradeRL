using System.Collections.Generic;
using UnityEngine;

public class HasJammedItemDrawer : MonoBehaviour
{
    public bool allowsJamChecks = false;
    public Inventory inventory;
    UIImageRaycasterPopup popup;
    int popupSpace;

    void Start()
    {
        SetupPopup();
    }

    void OnDestroy()
    {
        inventory.GetItems().ForEach(i =>
        {
            i.itemJammedEvent -= RecordJamChecks;
            i.jamChecksChanged -= RecordJamChecks;
        });
    }

    void SetupPopup()
    {
        popup = GetComponent<UIImageRaycasterPopup>();
        popupSpace = popup.ReserveSpace();

        RecordJamChecks();
        inventory.GetItems().ForEach(i =>
        {
            i.itemJammedEvent += RecordJamChecks;
            i.jamChecksChanged += RecordJamChecks;
        });
    }

    void RecordJamChecks()
    {
        if(allowsJamChecks)
        {
            var affected = inventory.GetItemsWithReducedJamChecks();
            if (affected.Count <= 0)
                popup.Record("Need a jammed item (or a clicked item) to use.", popupSpace);
            else
            {
                string affectedList = "";
                affected.ForEach(a => affectedList += a.GetName() + ", ");
                affectedList.Remove(affectedList.Length - 2);
                popup.Record(WriteAffectedString(affected), popupSpace);
            }
        }
        else
        {
            var affected = inventory.GetJammedItems();
            if (affected.Count <= 0)
                popup.Record("Need a jammed item to use.", popupSpace);
            else
            {
                popup.Record(WriteAffectedString(affected), popupSpace);
            }
        }
    }

    string WriteAffectedString(List<Item> items)
    {
        string affectedList = "Affects: ";
        for(int i = 0; i < items.Count; i++)
        {
            affectedList += items[i].GetName();
            if (i < items.Count - 1)
                affectedList += ", ";
        }
        return affectedList; 
    }
}

