using System;
using UnityEngine;

public class DistanceRestriction : Restriction, Visualizer  {
    public enum DistanceType
    {
        MustBeInMelee,
        MustBeAtRange,
    }
    public DistanceType type { private get; set; }
    [Inject] public ActiveLabelRequirements activeLabels { private get; set; }
    public Character character;

    public bool CanUse()
    {
        switch(type)
        {
            case DistanceType.MustBeInMelee:
                return character.IsInMelee || activeLabels.GetActiveLabels().Contains(AbilityLabel.MovesToMelee);
            case DistanceType.MustBeAtRange:
                return !character.IsInMelee || activeLabels.GetActiveLabels().Contains(AbilityLabel.MovesToRanged);
        }

        return true;
    }

    public void SetupVisualization(GameObject go)
    {
        var drawer = go.AddComponent<GenericRestrictionDrawer>();
        if(type == DistanceType.MustBeAtRange)
            drawer.text = "You must Move to Far Range to use this";
        else
            drawer.text = "You must Move to Close Range to use this";
        drawer.restriction = this;
    }
}
