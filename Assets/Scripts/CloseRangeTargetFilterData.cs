public class CloseRangeTargetFilterData : InputTargetFilterData {
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new CloseRangeTargetFilter();
        return filter;
    }
}

