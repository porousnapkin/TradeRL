public class TargetOccupiedInputFilterData : InputTargetFilterData { 
	public bool mustBeFriend = false;
	public bool mustBeFoe = false;

	public override InputTargetFilter Create(Character owner) {
		var f = DesertContext.StrangeNew<TargetOccupiedInputFilter>();
		f.mustBeFriend = mustBeFriend;
		f.mustBeFoe = mustBeFoe;
		f.myFaction = owner.myFaction;
		return f;
	}
}