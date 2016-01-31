using UnityEngine;
using System.Collections;

public class GainSuppliesEvent : StoryActionEvent {
	[Inject] public Inventory inventory { private get; set; }
	public int supplies = 0;

	public void Activate() {
		inventory.Supplies += supplies;
	}
}
