using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiWrittenTextField : MonoBehaviour
{
    public Text text;
    List<string> strings = new List<string>();

    void Start()
    {
        UpdateWriting();
    }

    public int ReserveSpace()
    {
        strings.Add("");
        return strings.Count - 1;
    }

    public void Write(string s, int fieldIndex)
    {
        strings[fieldIndex] = s;
        UpdateWriting();
    }

    void UpdateWriting()
    {
        text.text = "";
        for (int i = 0; i < strings.Count - 1; i++)
        {
            text.text += strings[i] + ", ";
        }

        if(strings.Count > 0)
            text.text += strings[strings.Count - 1];
    }
}