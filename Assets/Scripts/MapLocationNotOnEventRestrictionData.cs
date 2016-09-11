using UnityEngine;
using System.Collections;
using System;

public class MapLocationNotOnEventRestrictionData : RestrictionData
{
    public override Restriction Create(Character character)
    {
        return DesertContext.StrangeNew<MapLocationNotOnEventRestriction>();
    }
}
