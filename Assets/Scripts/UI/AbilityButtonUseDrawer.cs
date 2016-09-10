using UnityEngine;

public abstract class AbilityButtonUseDrawer : MonoBehaviour
{
    public abstract void CheckCost(AbilityCost cost);
    public abstract void CheckRestriction(Restriction restriction);
}