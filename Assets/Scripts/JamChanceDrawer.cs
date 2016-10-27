using UnityEngine;

public class JamChanceDrawer: MonoBehaviour
{
    public ItemData item;

    void Start()
    {
        var text = GetComponentInChildren<MultiWrittenTextField>();
        if(text == null)
            GameObject.Destroy(this);

        var fieldIndex = text.ReserveSpace();
        text.Write("Jam: " + Mathf.RoundToInt(item.jamChance * 100).ToString() + "%", fieldIndex);
    }
}

