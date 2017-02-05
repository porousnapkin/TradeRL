public class MapAbilityGunUnjammerActivator : MapAbilityActivator 
{
	[Inject] public Inventory inventory {private get; set; }

	public void Activate (System.Action callback)
	{
		var jammedItems = inventory.GetItemsWithReducedJamChecks();

        //TODO:
        //We want to pop-up a pickable list of items if more than one is available to let the user choose.

        jammedItems.ForEach(i => i.FixJam());

		callback();
	}
}
