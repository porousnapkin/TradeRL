public class ItemIsUnjammedRestrictionData : RestrictionData
{
    public ItemData item;

    public override Restriction Create(Character character)
    {
        var r = DesertContext.StrangeNew<ItemIsUnjammedRestriction>();
        r.item = item;
        return r;
    }
}

