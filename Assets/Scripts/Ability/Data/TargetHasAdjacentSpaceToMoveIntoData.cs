public class TargetHasAdjacentSpaceToMoveIntoData : InputTargetFilterData { 
	public override InputTargetFilter Create(Character owner) {
		return AbilityTargetPickerFactory.CreateTargetHasAdjacentSpacetoMoveInto();
	}
}