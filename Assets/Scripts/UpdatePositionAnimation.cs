using UnityEngine;
using System.Collections.Generic;

public class UpdatePositionAnimation : TargetedAnimation
{
    [Inject] public FactionManager factionManager { private get; set; }

    public void Play(Character target, System.Action finished, System.Action activated)
    {
        activated();

        var allies = factionManager.GetAllies(target);
        allies.Remove(target);
        var justTarget = new List<Character>();
        justTarget.Add(target);
        var positions = CombatView.GetNewPositions(allies, justTarget, target.myFaction);
        var newPosition = positions[0];

        var art = target.ownerGO;
        LeanTween.move(art, newPosition, GlobalVariables.moveTime).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => finished());
    }
}
