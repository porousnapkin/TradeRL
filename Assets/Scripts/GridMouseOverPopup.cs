using strange.extensions.mediation.impl;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridMouseOverPopup
{
    [Inject] public GridInputCollector gridInputCollector { private get; set; }
    Dictionary<Vector2, InputPopupHandler> handlers = new Dictionary<Vector2, InputPopupHandler>();
    InputPopupHandler activeHandler = null;

    [PostConstruct]
    public void PostConstruct()
    {
        gridInputCollector.mouserOverPositionEvent += CheckPosition;
    }

    ~GridMouseOverPopup()
    {
        foreach (var v in handlers.Values) v.Destroy();
        gridInputCollector.mouserOverPositionEvent -= CheckPosition;
    }

    void CheckPosition(Vector2 position)
    {
        InputPopupHandler handler;
        handlers.TryGetValue(position, out handler);
        if (activeHandler == handler)
            return;

        if(activeHandler != null)
            activeHandler.Hide();

        if(handler != null)
            handler.Show();

        activeHandler = handler;
    }

    public int ReserveSpace(Vector2 position)
    {
        return GetPopupHandler(position).ReserveSpace();
    }

    InputPopupHandler GetPopupHandler(Vector2 position)
    {
        InputPopupHandler handler;
        if (handlers.TryGetValue(position, out handler))
            return handler;

        handler = new InputPopupHandler();
        handlers[position] = handler;
        return handler; ;
    }

    public void Record(Vector2 position, string s, int fieldIndex = 0)
    {
        var handler = GetPopupHandler(position);
        handler.Record(s, fieldIndex);

        if (handler.IsEmpty())
            handlers.Remove(position);
    }

    public void Clear(Vector2 position, int fieldIndex = 0)
    {
        Record(position, "", fieldIndex);
    }
}