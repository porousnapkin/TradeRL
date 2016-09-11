using UnityEngine;

public class EffortCostDrawer : MonoBehaviour
{
    public EffortCost cost { private get; set; }
    MultiWrittenTextField text;
    int fieldIndex;

    void Start()
    {
        text = GetComponentInChildren<MultiWrittenTextField>();
        if(text == null)
            GameObject.Destroy(this);
        else
            fieldIndex = text.ReserveSpace();
    }

    void Update()
    {
        text.Write("-" + cost.amount + " " + cost.effortType, fieldIndex);
    }
}