public class GainTradeGoodEventData : StoryActionEventData
{
    public TownData townData;
    public int numTradeGoods = 1;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<GainTradeGoodEvent>();
        e.townData = townData;
        e.numTradeGoods = numTradeGoods;
        return e;
    }
}

