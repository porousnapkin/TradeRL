using UnityEngine;
using System.Collections.Generic;

public class SingleTargetInputPicker : AbilityTargetPicker {
    [Inject] public FactionManager factionManager { private get; set; }
    public List<InputTargetFilter> filters;
	System.Action< List<Character> > pickedCallback;
    TargetHighlighter targetHighlighter;
    TargetInputReciever inputReciever;
    List<Character> activeTargets;

    public SingleTargetInputPicker()
    {
        targetHighlighter = DesertContext.StrangeNew<TargetHighlighter>();
        inputReciever = DesertContext.StrangeNew<TargetInputReciever>();
    }

    List<Character> GetPossibleTargets()
    {
        var targets = new List<Character>(factionManager.EnemyMembers);
        targets.AddRange(factionManager.PlayerMembers);

        filters.ForEach(f => f.FilterOut(targets));

        return targets;
    }

	public void PickTargets(System.Action< List<Character> > pickedCallback) {
		this.pickedCallback = pickedCallback;

        activeTargets = GetPossibleTargets();
        targetHighlighter.HighlightTargets(activeTargets);
        inputReciever.CaptureTargetClicked(activeTargets, TargetPicked);
	}

    void TargetPicked(Character target)
    {
        targetHighlighter.RemoveHighlights();
        inputReciever.FinishTargetClickCaptures();
        pickedCallback(new List<Character>(new Character[1] { target }));
    }

	public bool HasValidTarget() { 
        return GetPossibleTargets().Count > 0;
	}
}