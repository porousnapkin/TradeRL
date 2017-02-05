using UnityEngine;
using System.Collections;

public class HasJammedGunRestrictionData : RestrictionData {
    public bool allowsJamChecks = false;

	public override Restriction Create (Character character)
	{
		var r = DesertContext.StrangeNew<HasJammedGunRestriction>();
        r.allowsJamChecks = allowsJamChecks;
		return r;
	}
}
