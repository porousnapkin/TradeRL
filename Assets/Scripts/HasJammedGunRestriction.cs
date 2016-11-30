using UnityEngine;
using System.Collections;

public class HasJammedGunRestriction : Restriction {
	[Inject] public Inventory inventory {private get; set;}
	
	public bool CanUse ()
	{
		return inventory.GetJammedItems().Count > 0;
	}

	public void SetupVisualization (GameObject go)
	{
		//TODO:
	}
}
