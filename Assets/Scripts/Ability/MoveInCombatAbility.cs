using UnityEngine;
using System.Collections.Generic;

public class MoveInCombatAbility : AbilityActivator {
    public enum WhereToMove
    {
        ToMelee,
        ToRanged
    }

    public WhereToMove whereToMove;
    public CombatController controller;
    bool hasFinished = false;
    System.Action callback;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        hasFinished = false;
        callback = finishedAbility;
        Debug.Log("Move!");

        targets.ForEach(t =>
        {
            t.IsInMelee = whereToMove == WhereToMove.ToMelee;
            animation.Play(t, Finished, () => {});
        });
	}

    void Finished()
    {
        if (hasFinished)
            return;

        hasFinished = true;
        callback();
    }
}
