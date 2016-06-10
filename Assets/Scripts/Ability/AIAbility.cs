using UnityEngine;
using System.Collections.Generic;

public class AIAbility {
	public int cooldown = 1;
	int turnsOnCooldown = 0;
	[Inject] public DooberFactory dooberFactory { private get; set; }
	public AICombatController controller { private get; set; }
	public AbilityTargetPicker targetPicker { private get; set; }
	public AbilityActivator activator { private get; set; }
	public LocationTargetedAnimation animation { private get; set; }
	public string displayMessage { private get; set; }

	public void Setup() {
		controller.ActEvent += AdvanceCooldown;
	}
	
	~AIAbility() {
        controller.ActEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

	public void PerformAction(System.Action callback) {
		turnsOnCooldown = cooldown;
		targetPicker.PickTargets(TargetsPicked);

		var worldPos = controller.character.Position;
		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)worldPos.x, (int)worldPos.y);

		dooberFactory.CreateAbilityMessageDoober(messageAnchor, displayMessage);

        callback();
	}	

	void TargetsPicked(List<Vector2> targets) {
		activator.Activate(targets, animation, ActionFinished);
	}

	void ActionFinished() {
		controller.EndTurn();
	}

	public bool CanUse() {
		return turnsOnCooldown <= 0 && targetPicker.HasValidTarget();
	}
}