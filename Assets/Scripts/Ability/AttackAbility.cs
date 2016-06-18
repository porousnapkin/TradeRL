using UnityEngine;
using System.Collections.Generic;

public class AttackAbility : AbilityActivator {
    [Inject]public CombatModule combatModule { private get; set; }
    public CombatController controller;
    public bool isRangedAttack = false;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        Character target = targets[Random.Range(0,targets.Count)];

		animation.Play(target, finishedAbility, () => Hit(target));
	}

	void Hit(Character target) {
        combatModule.Attack(controller.GetCharacter(), target, isRangedAttack);
	}
}
