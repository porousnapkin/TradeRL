public class DistanceRestrictionData : RestrictionData
{
    public DistanceRestriction.DistanceType type;

    public override Restriction Create(Character character)
    {
        var distanceRestriction = DesertContext.StrangeNew<DistanceRestriction>();
        distanceRestriction.type = this.type;
        distanceRestriction.character = character;

        return distanceRestriction;
    }
}
