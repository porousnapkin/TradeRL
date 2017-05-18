public class HasXInitiativeRestrictionData : RestrictionData
{
    public int initiativeRequirement = 5;

    public override Restriction Create(Character character)
    {
        var restriction = DesertContext.StrangeNew<HasXInitiativeRestriction>();
        restriction.initiativeRequriement = initiativeRequirement;
        return restriction;
    }
}
