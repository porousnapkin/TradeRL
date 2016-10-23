using System;
using System.Collections.Generic;

public class JamItemAbilityActivator : AbilityActivator
{
    [Inject]public Inventory inventory { private get; set; }
    public ItemData item { private get; set; }

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        var actualItem = inventory.GetItemByName(item.itemName);
        if (!actualItem.IsJammed())
            actualItem.JamCheck();

        finishedAbility();
    }
}

