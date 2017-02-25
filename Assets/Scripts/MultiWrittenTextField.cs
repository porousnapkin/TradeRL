using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiWrittenTextField : MonoBehaviour
{
    public Text text;
    MultiWrittenString multiString;

    void Start()
    {
        multiString = new MultiWrittenString(", ");
        UpdateWriting();
    }

    public int ReserveSpace()
    {
        return multiString.ReserveSpace();
    }

    public void Record(string s, int fieldIndex)
    {
        multiString.Record(s, fieldIndex);
        UpdateWriting();
    }

    void UpdateWriting()
    {
        text.text = multiString.Write();
    }
}