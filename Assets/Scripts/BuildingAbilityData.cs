using UnityEngine;
using System.Collections;

public abstract class BuildingAbilityData : ScriptableObject {
	public abstract BuildingAbility Create();
}

public class TradingPostAbilityData : BuildingAbilityData {
	public override BuildingAbility Create() {
		return DesertContext.StrangeNew<TradingPost>();
	}
}
