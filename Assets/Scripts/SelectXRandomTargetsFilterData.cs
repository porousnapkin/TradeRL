public class SelectXRandomTargetsFilterData : InputTargetFilterData
{
    public int numberToSelect;

    public override InputTargetFilter Create(Character owner)
    {
        var filter = new SelectXRandomTargetsFilter();
        filter.numberToSelect = numberToSelect;
        return filter;
    }
}

