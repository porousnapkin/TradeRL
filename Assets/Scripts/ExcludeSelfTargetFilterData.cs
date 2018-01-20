public class ExcludeSelfTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new ExcludeSelfTargetFilter();
        filter.self = owner;
        return filter;
    }
}

