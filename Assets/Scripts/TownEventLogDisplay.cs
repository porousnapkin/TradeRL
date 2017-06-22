using strange.extensions.mediation.impl;
using UnityEngine;

public class TownEventLogDisplay : DesertView {
    public Transform eventParent;
    public GameObject eventPrefab;

    public void AddTextEvent(string description, string popupText)
    {
        var eventGO = GameObject.Instantiate(eventPrefab, eventParent) as GameObject;
        eventGO.GetComponentInChildren<TownEventTextDisplay>().Setup(description, popupText);
    }
}

public class TownEventLogDisplayMediator : Mediator
{
    [Inject] public TownEventLogDisplay view { private get; set; }
    [Inject] public TownEventLog model { private get; set; }

    public override void OnRegister()
    {
        base.OnRegister();

        model.textEventAddedEvent += view.AddTextEvent;
    }

    public override void OnRemove()
    {
        base.OnRemove();

        model.textEventAddedEvent -= view.AddTextEvent;
    }
}

public class TownEventLog
{
    public event System.Action<string, string> textEventAddedEvent = delegate { };

    public void AddTextEvent(string description, string popupText)
    {
        textEventAddedEvent(description, popupText);
    }
}
