using UnityEngine;

public abstract class TownBenefitData : ScriptableObject
{
    public abstract TownBenefit Create(Town town);
}