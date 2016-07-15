using UnityEngine;
using System.Collections;

public class DistanceRestrictionData : AbilityRestrictionData
{
    public DistanceRestriction.DistanceType type;

    public override AbilityRestriction Create(Character character)
    {
        var distanceRestriction = DesertContext.StrangeNew<DistanceRestriction>();
        distanceRestriction.type = this.type;
        distanceRestriction.character = character;

        return distanceRestriction;
    }
}
