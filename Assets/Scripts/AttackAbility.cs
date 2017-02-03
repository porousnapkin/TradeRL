using UnityEngine;
using System.Collections.Generic;

public class AttackAbility : AbilityActivator, Visualizer {
    [Inject]public CombatModule combatModule { private get; set; }
    public CombatController controller;
    int numToAttack = 0;
    int numAttacked = 0;
    System.Action callback;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        callback = finishedAbility;
        numToAttack = targets.Count;

        targets.ForEach((t) =>
        {
            animation.Play(t, Finished, () => Hit(t));
        });
	}

    void Finished()
    {
        numAttacked++;
        if (numAttacked >= numToAttack)
            callback();
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
