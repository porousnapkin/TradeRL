using UnityEngine;
using System.Collections;

public class GainCoinsEvent : StoryActionEvent
{
    [Inject]
    public Inventory inventory { private get; set; }
    public int coins = 0;

    public void Activate()
    {
        inventory.Gold += coins;
    }
}
