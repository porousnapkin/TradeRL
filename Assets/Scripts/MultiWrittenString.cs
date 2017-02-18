using System.Collections.Generic;

public class MultiWrittenString
{
    string separator;
    List<string> strings = new List<string>();
    public event System.Action stringAltered = delegate { };

    public MultiWrittenString(string separator)
    {
        this.separator = separator;
    }

    public int ReserveSpace()
    {
        strings.Add("");
        return strings.Count - 1;
    }

    public void Record(string s, int fieldIndex)
    {
        strings[fieldIndex] = s;
        stringAltered();
    }

    public string Write()
    {
        var text = "";
        bool first = true;
        for (int i = 0; i < strings.Count; i++)
        {
            if (strings[i] != "")
            {
                if (!first)
                    text += separator;
                text += strings[i];
                first = false;
            }
        }

        return text;
    }
}

