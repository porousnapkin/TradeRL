using UnityEngine;
using System.Collections.Generic;
using System;

public class CustomAttackAbility : AbilityActivator, Visualizer
{
    [Inject]
    public CombatModule combatModule { private get; set; }
    public CombatController controller;
    public int minDamage = 10;
    public int maxDamage = 12;
    public bool canCrit = true;
    int numToAttack = 0;
    int numAttacked = 0;
    System.Action callback;

    public void PrepareActivation(List<Character> targets, TargetedAnimation animation, Action preparedCallback)
    {
        var bd = new ModifiedBaseDamage();
        bd.minDamage = minDamage;
        bd.maxDamage = maxDamage;
        controller.character.attackModule.OverrideBaseDamage(bd);
        preparedCallback();
    }

    public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility)
    {
        callback = finishedAbility;
        numToAttack = targets.Count;

        targets.ForEach((t) =>
        {
            animation.Play(t, Finished, () => Hit(t));
        });
    }

    void Finished()
    {
        controller.character.attackModule.RemoveBaseDamageOverride();

        numAttacked++;
        if (numAttacked >= numToAttack)
            callback();
    }

    void Hit(Character target)
    {
        combatModule.Attack(controller.GetCharacter(), target);
    }

    public void SetupVisualization(GameObject go)
    {
        var drawer = go.AddComponent<AttackAbilityDrawer>();
        drawer.minDamage = minDamage;
        drawer.maxDamage = maxDamage;
    }
}
