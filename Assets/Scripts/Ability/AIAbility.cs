using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AIAbility {
	public int cooldown = 1;
	int turnsOnCooldown = 0;
	[Inject] public DooberFactory dooberFactory { private get; set; }
	public CombatController controller { private get; set; }
	public AbilityTargetPicker targetPicker { private get; set; }
	public AbilityActivator activator { private get; set; }
	public TargetedAnimation animation { private get; set; }
	public string displayMessage { private get; set; }
    public List<AbilityRestriction> restrictions { private get; set; }
    System.Action callback;

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
        this.callback = callback;
		turnsOnCooldown = cooldown;
		targetPicker.PickTargets(TargetsPicked);

		var worldPos = controller.character.Position;
		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)worldPos.x, (int)worldPos.y);

		dooberFactory.CreateAbilityMessageDoober(messageAnchor, displayMessage);
	}	

	void TargetsPicked(List<Character> targets) {
		activator.Activate(targets, animation, ActionFinished);
	}

	void ActionFinished() {
        callback();
	}

	public bool CanUse() {
		return turnsOnCooldown <= 0 && targetPicker.HasValidTarget() && restrictions.All(r => r.CanUse());
	}
}