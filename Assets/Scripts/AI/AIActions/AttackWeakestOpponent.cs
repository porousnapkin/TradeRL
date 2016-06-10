using UnityEngine;

public class AttackWeakestOpponent : AIAction {
	public AICombatController controller { private get; set; }

	[Inject(DesertPathfinder.COMBAT)] public DesertPathfinder pathfinder { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public FactionManager factionManager { private get; set; }

	public int GetActionWeight() { 
		if(GetTarget() == null)
			return 0; 
		else
			return 1;
	}

	public void PerformAction(System.Action callback) {
		Character target = GetTarget();

		if(target != null)
			controller.Attack(target, () => {
                    callback();
                });
		else {
			Debug.LogError("AI ERROR: Attempted to attack when no target was found");
			controller.EndTurn();
		}
	} 

	Character GetTarget() {
		var opponents = factionManager.GetOpponents(controller.character);
        
		opponents.Sort((first, second) => first.health.Value - second.health.Value);

		return opponents[0];
	}
}