public class GainCoinsEvent : StoryActionEvent
{
    [Inject]
    public Inventory inventory { private get; set; }
    public int coins = 0;

    public void Activate(System.Action callback)
    {
        inventory.Gold += coins;
        callback();
    }
}

