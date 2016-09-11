using UnityEngine;
using System.Collections.Generic;

public class CustomAttackAbility : AbilityActivator
{
    [Inject]
    public CombatModule combatModule { private get; set; }
    public CombatController controller;
    public int minDamage = 10;
    public int maxDamage = 12;
    public bool canCrit = true;
    public bool isRangedAttack = false;

    public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility)
    {
        Character target = targets[Random.Range(0, targets.Count)];

        animation.Play(target, finishedAbility, () => Hit(target));
    }

    void Hit(Character target)
    {
        combatModule.CustomAttack(controller.GetCharacter(), target, minDamage, maxDamage, isRangedAttack, canCrit);
    }
}
