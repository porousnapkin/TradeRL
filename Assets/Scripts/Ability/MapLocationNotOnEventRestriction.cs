using UnityEngine;
using System.Collections;
using System;

public class MapLocationNotOnEventRestriction : Restriction
{
    [Inject] public MapPlayerController mapPlayerController { private get; set; }
    [Inject] public MapGraph mapGraph { private get; set; }

    public bool CanUse()
    {
        var pos = mapPlayerController.position;
        return !mapGraph.DoesLocationHaveEvent((int) pos.x, (int) pos.y);
    }
}
