using System;
using UnityEngine;

public abstract class StoryActionEventData : ScriptableObject {
	public abstract StoryActionEvent Create();
}

class ChangeItemsEventData : StoryActionEventData
{
    public ItemData item;
    public int quantityChange;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<ChangeItemsEvent>();
        e.item = item;
        e.quantityChange = quantityChange;
        return e;
    }
}