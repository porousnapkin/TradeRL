using UnityEngine;
using System.Collections;
using System;

public class GainHealthEvent : StoryActionEvent
{
    public enum CountingType
    {
        Integer,
        PercentOfMax
    }

    [Inject] public PlayerCharacter player { private get; set; }
    public int amount { private get; set; }
    public float percent { private get; set; }
    public CountingType counting { private get; set; }

    public void Activate(System.Action callback)
    {
        Heal(player.GetCharacter().health);
        callback();
    }

    void Heal(Health health)
    {
        switch (counting)
        {
            case CountingType.Integer:
                health.Heal(amount);
                break;
            case CountingType.PercentOfMax:
                health.Heal(Mathf.RoundToInt(health.MaxValue * percent));
                break;
        }
    }
}
