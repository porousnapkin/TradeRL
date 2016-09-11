using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetInputReciever {
    List<Character> activeTargets;
    System.Action<Character> activeCallback;

    public void CaptureTargetClicked(List<Character> targets, System.Action<Character> pickedCallback)
    {
        activeTargets = targets;
        activeCallback = pickedCallback;

        activeTargets.ForEach(t => {
            var input = t.ownerGO.GetComponentInChildren<CharacterMouseInput>();
            input.mouseDown += Clicked;
        });
    }

    void Clicked(Character clicked)
    {
        activeCallback(clicked);
    }

    public void FinishTargetClickCaptures()
    {
        activeTargets.ForEach(t =>
        {
            var input = t.ownerGO.GetComponentInChildren<CharacterMouseInput>();
            input.mouseDown -= Clicked;
        });
    }
}
