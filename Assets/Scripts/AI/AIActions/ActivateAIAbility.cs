using UnityEngine;
using System.Collections.Generic;

public class ActivateAIAbility : AIAction {
	public AIAbility ability;
	
	public int GetActionWeight() { 
		if(ability.CanUse())
			return 1; 
		else
			return 0;
	}

	public void PerformAction() {
		ability.PerformAction();
	} 
}