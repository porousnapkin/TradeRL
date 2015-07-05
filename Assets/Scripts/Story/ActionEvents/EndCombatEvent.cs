using UnityEngine;

public class EndCombatEvent : StoryActionEvent {
	public Combat activeCombat;

	public void Activate() {
		activeCombat.EndPrematurely();
	}		
}