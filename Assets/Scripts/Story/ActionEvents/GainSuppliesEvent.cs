using UnityEngine;
using System.Collections;

public class GainSuppliesEvent : StoryActionEvent {
	public int supplies = 0;
	public Inventory inventory;

	public void Activate() {
		inventory.Supplies += supplies;
	}
}
