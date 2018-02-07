public class HasXMoneyRestrictionData : RestrictionData
{
    public int amount = 10;
    public override Restriction Create(Character character)
    {
        var r = DesertContext.StrangeNew<HasXMoneyRestriction>();
        r.amount = amount;
        return r;
    }
}

