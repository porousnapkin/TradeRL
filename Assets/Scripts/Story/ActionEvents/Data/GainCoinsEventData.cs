using UnityEngine;
using System.Collections;

public class GainCoinsEventData : StoryActionEventData
{
    public int numCoins;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<GainCoinsEvent>();
        e.coins = numCoins;
        return e;
    }
}
