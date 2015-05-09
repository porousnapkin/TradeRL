
public class NoCostData : AbilityCostData {
	public override AbilityCost Create(Character owner) { return new NoCost(); }
}