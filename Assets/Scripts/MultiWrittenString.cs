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
        for (int i = 0; i < strings.Count - 1; i++)
            if(strings[i] != "")
                text += strings[i] + separator;

        if (strings.Count > 0)
            text += strings[strings.Count - 1];

        return text;
    }
}

