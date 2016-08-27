using UnityEngine;
using System.Collections;
using System;

public class MapLocationNotOnEventRestrictionData : AbilityRestrictionData
{
    public override AbilityRestriction Create(Character character)
    {
        return DesertContext.StrangeNew<MapLocationNotOnEventRestriction>();
    }
}
