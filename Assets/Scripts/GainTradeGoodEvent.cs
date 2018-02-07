public class GainTradeGoodEvent: StoryActionEvent
{
    [Inject]
    public Inventory inventory { private get; set; }
    [Inject]
    public Towns towns { private get; set; }
    public TownData townData;
    public int numTradeGoods;

    public void Activate(System.Action callback)
    {
        inventory.GainTradeGood(towns.GetTown(townData), numTradeGoods, 0);
        callback();
    }
}

