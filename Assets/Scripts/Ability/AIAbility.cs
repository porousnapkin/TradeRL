using UnityEngine;
using System.Collections.Generic;

public class AIAbility {
	public int cooldown = 1;
	int turnsOnCooldown = 0;
	[Inject] public TurnManager turnManager { private get; set; }
	[Inject] public DooberFactory dooberFactory { private get; set; }
	public AIController controller { private get; set; }
	public AbilityTargetPicker targetPicker { private get; set; }
	public AbilityActivator activator { private get; set; }
	public LocationTargetedAnimation animation { private get; set; }
	public string displayMessage { private get; set; }


	public void Setup(AIController controller, AIAbilityData data) {
		turnManager.TurnEndedEvent += AdvanceCooldown;

		targetPicker = data.targetPicker.Create(controller.character);
		activator = data.activator.Create(controller.character);
		cooldown = data.cooldown;
		controller = controller;
		animation = data.animation.Create(controller.character);
		displayMessage = data.displayMessage;
	}
	
	~AIAbility() { 
		turnManager.TurnEndedEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

	public void PerformAction() {
		turnsOnCooldown = cooldown;
		targetPicker.PickTargets(TargetsPicked);

		var worldPos = controller.character.Position;
		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)worldPos.x, (int)worldPos.y);

		dooberFactory.CreateAbilityMessageDoober(messageAnchor, displayMessage);
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