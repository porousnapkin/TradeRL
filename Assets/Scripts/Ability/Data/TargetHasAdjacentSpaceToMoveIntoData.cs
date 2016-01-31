public class TargetHasAdjacentSpaceToMoveIntoData : InputTargetFilterData { 
	public override InputTargetFilter Create(Character owner) {
		return DesertContext.StrangeNew<TargetHasAdjacentSpaceToMoveInto>();
	}
}