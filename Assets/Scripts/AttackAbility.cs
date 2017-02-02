using UnityEngine;
using System.Collections.Generic;

public class AttackAbility : AbilityActivator, Visualizer {
    [Inject]public CombatModule combatModule { private get; set; }
    public CombatController controller;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        Character target = targets[Random.Range(0,targets.Count)];

		animation.Play(target, finishedAbility, () => Hit(target));
	}

	void Hit(Character target) {
        combatModule.Attack(controller.GetCharacter(), target);
	}

    public void SetupVisualization(GameObject go)
    {
        var attackModule = controller.GetCharacter().attackModule;

        var drawer = go.AddComponent<AttackAbilityDrawer>();
        drawer.minDamage = attackModule.minDamage;
        drawer.maxDamage = attackModule.maxDamage;
    }
}
