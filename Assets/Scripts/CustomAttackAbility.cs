using UnityEngine;
using System.Collections.Generic;

public class CustomAttackAbility : AbilityActivator, Visualizer
{
    [Inject]
    public CombatModule combatModule { private get; set; }
    public CombatController controller;
    public int minDamage = 10;
    public int maxDamage = 12;
    public bool canCrit = true;

    public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility)
    {
        Character target = targets[Random.Range(0, targets.Count)];

        animation.Play(target, finishedAbility, () => Hit(target));
    }

    void Hit(Character target)
    {
        combatModule.CustomAttack(controller.GetCharacter(), target, minDamage, maxDamage, canCrit);
    }

    public void SetupVisualization(GameObject go)
    {
        var attackModule = controller.GetCharacter().attackModule;

        var drawer = go.AddComponent<AttackAbilityDrawer>();
        drawer.minDamage = minDamage;
        drawer.maxDamage = maxDamage;
    }
}
