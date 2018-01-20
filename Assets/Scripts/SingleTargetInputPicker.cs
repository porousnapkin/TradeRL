using System.Collections.Generic;
using UnityEngine;

public class SingleTargetInputPicker : AbilityTargetPicker {
    [Inject] public FactionManager factionManager { private get; set; }
    [Inject] public TargetHighlighter targetHighlighter { private get; set; }
    public List<InputTargetFilter> filters;
	System.Action< List<Character> > pickedCallback;
    TargetInputReciever inputReciever;
    List<Character> activeTargets;
    Character pickedTarget;

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

    public void PrePickTargets(System.Action<List<Character>> targetsPicked)
    {
		this.pickedCallback = targetsPicked;

        activeTargets = GetPossibleTargets();
        targetHighlighter.HighlightTargets(activeTargets);
        inputReciever.CaptureTargetClicked(activeTargets, TargetPicked);
    }

    public void PickTargets(System.Action< List<Character> > pickedCallback) {
        activeTargets = GetPossibleTargets();
        if (activeTargets.Contains(pickedTarget))
            pickedCallback(new List<Character>(new Character[1] { pickedTarget }));
        else if (activeTargets.Count > 0)
            pickedCallback(new List<Character>(new Character[1] { activeTargets[Random.Range(0, activeTargets.Count)] }));
        else
            pickedCallback(new List<Character>());
	}

    void TargetPicked(Character target)
    {
        pickedTarget = target;
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