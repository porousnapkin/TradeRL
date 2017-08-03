using strange.extensions.mediation.impl;
using UnityEngine;

public class GridMouseOverPopup
{
    [Inject] public GridInputCollector gridInputCollector { private get; set; }
    InputPopupHandler handler = new InputPopupHandler();
    Vector2 location = -Vector2.one;
    public bool enabled = true;

    [PostConstruct]
    public void PostConstruct()
    {
        gridInputCollector.mouserOverPositionEvent += CheckPosition;
    }

    public void SetLocation(Vector2 location)
    {
        this.location = location;
    }

    ~GridMouseOverPopup()
    {
        handler.Destroy();
        gridInputCollector.mouserOverPositionEvent -= CheckPosition;
    }

    void CheckPosition(Vector2 position)
    {
        if (enabled)
            handler.Show();
        else
            handler.Hide();
    }

    public int ReserveSpace()
    {
        return handler.ReserveSpace();
    }

    public void Record(string s, int fieldIndex = 0)
    {
        handler.Record(s, fieldIndex);
    }
}