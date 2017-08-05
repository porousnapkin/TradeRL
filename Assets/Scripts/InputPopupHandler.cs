public class InputPopupHandler
{
    public string defaultText = "";
    MultiWrittenString multiString = new MultiWrittenString("\n\n");
    bool active = false;

    public void Setup()
    {
        multiString.stringAltered += UpdateDescription;

        if (defaultText != "")
        {
            ReserveSpace();
            Record(defaultText, 0);
        }
        else
            UpdateDescription();
    }

    public void Destroy()
    {
        multiString.stringAltered -= UpdateDescription;
        if (active)
        {
            SingletonPopup.Instance.DoneWithPopup();
            active = false;
        }
    }

    void UpdateDescription()
    {
        if (active)
            SingletonPopup.Instance.UpdateDescription(multiString.Write());
    }

    public int ReserveSpace()
    {
        return multiString.ReserveSpace();
    }

    public void Record(string s, int fieldIndex = 0)
    {
        multiString.Record(s, fieldIndex);
    }

    public void Show()
    {
        if (active || IsEmpty())
            return;

        SingletonPopup.Instance.ShowPopup(multiString.Write());
        active = true;
    }

    public void Hide()
    {
        if (!active || IsEmpty())
            return;

        SingletonPopup.Instance.DoneWithPopup();
        active = false;
    }

    public bool IsEmpty()
    {
        var description = multiString.Write();
        return description == "" || description == null;
    }
}
