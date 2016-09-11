public class CanRestRestrictionData : RestrictionData
{
    public override Restriction Create(Character character)
    {
        return DesertContext.StrangeNew<CanRestRestriction>();
    }
}