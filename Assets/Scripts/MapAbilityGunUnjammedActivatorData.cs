using UnityEngine;
using System.Collections;

public class MapAbilityGunUnjammedActivatorData : MapAbilityActivatorData 
{
	public override MapAbilityActivator Create ()
	{
		var unjammer = DesertContext.StrangeNew<MapAbilityGunUnjammerActivator>();

		return unjammer;
	}
}
