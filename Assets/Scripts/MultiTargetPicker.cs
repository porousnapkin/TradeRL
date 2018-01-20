using System;
using System.Collections.Generic;

public class MultiTargetPicker : AbilityTargetPicker
{
    [Inject] public FactionManager factionManager { private get; set; }
    public List<InputTargetFilter> filters;

    public bool HasValidTarget()
    {
        return GetAllTargets().Count > 0;
    }

    List<Character> GetAllTargets()
    {
        var targets = new List<Character>(factionManager.EnemyMembers);
        targets.AddRange(factionManager.PlayerMembers);
        targets.RemoveAll(t => t.health.Value <= 0);

        filters.ForEach(f => f.FilterOut(targets));

        return targets;
    }

    public void PrePickTargets(Action<List<Character>> targetsPicked)
    {
        targetsPicked(GetAllTargets());
    }

    public void PickTargets(Action<List<Character>> pickedCallback)
    {
        pickedCallback(GetAllTargets());
    }

    public void CancelPicking()
    {
    }
}

