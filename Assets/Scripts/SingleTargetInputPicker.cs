using System;
using System.Collections.Generic;

public class SingleTargetInputPicker : AbilityTargetPicker {
    [Inject] public FactionManager factionManager { private get; set; }
    [Inject] public TargetHighlighter targetHighlighter { private get; set; }
    public List<InputTargetFilter> filters;
	System.Action< List<Character> > pickedCallback;
    TargetInputReciever inputReciever;
    List<Character> activeTargets;

    public SingleTargetInputPicker()
    {
        inputReciever = DesertContext.StrangeNew<TargetInputReciever>();
    }

    List<Character> GetPossibleTargets()
    {
        var targets = new List<Character>(factionManager.EnemyMembers);
        targets.AddRange(factionManager.PlayerMembers);
        targets.RemoveAll(t => t.health.Value <= 0);

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
        targetHighlighter.RemoveAllHighlights();
        inputReciever.FinishTargetClickCaptures();
        pickedCallback(new List<Character>(new Character[1] { target }));
    }

	public bool HasValidTarget() { 
        return GetPossibleTargets().Count > 0;
	}

    public void CancelPicking()
    {
        targetHighlighter.RemoveAllHighlights();
        inputReciever.FinishTargetClickCaptures();
    }
}