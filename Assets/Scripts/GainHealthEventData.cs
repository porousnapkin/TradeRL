using UnityEngine;
using System.Collections;
using System;

public class GainHealthEventData : StoryActionEventData
{
    public GainHealthEvent.CountingType countingType;
    public int amount = 0;
    public float percent = 0.1f;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<GainHealthEvent>();
        e.counting = countingType;
        e.amount = amount;
        e.percent = percent;
        return e;
    }
}
