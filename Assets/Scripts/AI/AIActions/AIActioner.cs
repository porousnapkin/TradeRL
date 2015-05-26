using System.Collections.Generic;

public class AIActioner {
	List<AIAction> actions = new List<AIAction>();

	public void AddAction(AIAction action) {
		actions.Add(action);
	}

	public AIAction PickAction() {
		int maxWeight = 0;	
		AIAction bestAction = null;

		foreach(var action in actions) {
			var weight = action.GetActionWeight();
			if(weight > maxWeight) {
				bestAction = action;
				maxWeight = weight;
			}
		}

		return bestAction;
	}
}