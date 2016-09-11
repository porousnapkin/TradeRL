using UnityEngine;
using System.Collections.Generic;

public class AIAbilityTargetPicker : AbilityTargetPicker
{
    [Inject]
    public FactionManager factionManager { private get; set; }
    public List<InputTargetFilter> filters;

    public void PickTargets(System.Action<List<Character>> pickedCallback)
    {
        pickedCallback(GetPossibleTargets());
    }

    List<Character> GetPossibleTargets()
    {
        var targets = new List<Character>(factionManager.EnemyMembers);
        targets.AddRange(factionManager.PlayerMembers);

        filters.ForEach(f => f.FilterOut(targets));

        return targets;
    }

    public bool HasValidTarget()
    {
        return GetPossibleTargets().Count > 0;
    }
}
