using UnityEngine;

public class HasJammedGunRestriction : Restriction, Visualizer {
	[Inject] public Inventory inventory {private get; set;}
    public bool allowsJamChecks = false;

    public bool CanUse ()
	{
        if (allowsJamChecks && inventory.GetItemsWithReducedJamChecks().Count > 0)
            return true;
		return inventory.GetJammedItems().Count > 0;
	}

	public void SetupVisualization (GameObject go)
	{
        var drawer = go.AddComponent<HasJammedItemDrawer>();
        drawer.inventory = inventory;
        drawer.allowsJamChecks = allowsJamChecks;
    }
}

