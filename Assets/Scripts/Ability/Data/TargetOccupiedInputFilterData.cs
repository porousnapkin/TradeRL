public class TargetOccupiedInputFilterData : InputTargetFilterData { 
	public override InputTargetFilter Create(Character owner) {
		var f = new TargetOccupiedInputFilter();
		f.mapGraph = MapGraph.Instance;
		return f;
	}
}