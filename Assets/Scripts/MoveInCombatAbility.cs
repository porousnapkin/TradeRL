using UnityEngine;
using System.Collections.Generic;

public class MoveInCombatAbility : AbilityActivator {
    public enum WhereToMove
    {
        ToMelee,
        ToRanged,
        ToOppositeSpot
    }

    public WhereToMove whereToMove;
    public CombatController controller;
    public bool justMoveMe = false;
    bool hasFinished = false;
    System.Action callback;

	public void Activate(List<Character> targets, TargetedAnimation animation, System.Action finishedAbility) {
        hasFinished = false;
        callback = finishedAbility;

        if (justMoveMe)
            HandleMove(controller.GetCharacter(), animation);
        else
            targets.ForEach(t => HandleMove(t, animation));
	}

    void HandleMove(Character t, TargetedAnimation animation)
    {
        if (whereToMove == WhereToMove.ToOppositeSpot)
            t.IsInMelee = !t.IsInMelee;
        else
            t.IsInMelee = whereToMove == WhereToMove.ToMelee;
        animation.Play(t, Finished, () => { });
    }

    void Finished()
    {
        if (hasFinished)
            return;

        hasFinished = true;
        callback();
    }
}
