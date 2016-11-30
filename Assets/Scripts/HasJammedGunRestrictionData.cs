using UnityEngine;
using System.Collections;

public class HasJammedGunRestrictionData : RestrictionData {
	
	public override Restriction Create (Character character)
	{
		var r = DesertContext.StrangeNew<HasJammedGunRestriction>();
		return r;
	}
}
