using System;
using System.Collections.Generic;

public class JamItemAbilityActivator : AbilityActivator
{
    [Inject]public Inventory inventory { private get; set; }
    [Inject]public GlobalTextArea textArea { private get; set; }
    [Inject]public DooberFactory dooberFactory { private get; set; }
    public ItemData item { private get; set; }
    public Character character { private get; set; }
    Item actualItem;

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        actualItem = inventory.GetItemByName(item.itemName);
        actualItem.itemJammedEvent += RespondToJammed;
        actualItem.jamChecksChanged += RespondToChecksChanged;

        if (!actualItem.IsJammed())
            actualItem.JamCheck();


        actualItem.itemJammedEvent -= RespondToJammed;
        actualItem.jamChecksChanged -= RespondToChecksChanged;
        finishedAbility();
    }

    void RespondToChecksChanged()
    {
        dooberFactory.CreateAbilityMessageDoober(character.ownerGO.transform.position, "Click");

        textArea.AddLine(actualItem.GetName() + " clicks uncomfortably...");
    }

    void RespondToJammed()
    {
        dooberFactory.CreateAbilityMessageDoober(character.ownerGO.transform.position, "JAMMED");
		textArea.AddLine(actualItem.GetName() + " jammed!");
    }
}

