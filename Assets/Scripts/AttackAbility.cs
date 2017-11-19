using UnityEngine;
using System.Collections.Generic;
using System;

public class AttackAbility : AbilityActivator, Visualizer {
    [Inject]public CombatModule combatModule { private get; set; }
    public int numberOfAttacksPerTarget { private get; set; }
    public CombatController controller { private get; set; }
    System.Action callback;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        callback = finishedAbility;

        animation.Play(targets[0], FinishedAnim, () => ResolveHits(targets));
	}

    private void ResolveHits(List<Character> targets)
    {
        targets.ForEach((t) =>
        {
            for(int i = 0; i < numberOfAttacksPerTarget; i++)
                combatModule.Attack(controller.GetCharacter(), t);
        });
    }

    void FinishedAnim()
    {
        callback();
    }

    public void SetupVisualization(GameObject go)
    {
        var attackModule = controller.GetCharacter().attackModule;

        var drawer = go.AddComponent<AttackAbilityDrawer>();
        drawer.minDamage = attackModule.minDamage;
        drawer.maxDamage = attackModule.maxDamage;
    }
}
