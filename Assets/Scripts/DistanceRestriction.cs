using System;
using UnityEngine;

public class DistanceRestriction : Restriction {
    public enum DistanceType
    {
        MustBeInMelee,
        MustBeAtRange,
    }
    public DistanceType type { private get; set; }
    public Character character;

    public bool CanUse()
    {
        switch(type)
        {
            case DistanceType.MustBeInMelee:
                return character.IsInMelee;
            case DistanceType.MustBeAtRange:
                return !character.IsInMelee;
        }

        return true;
    }
}
