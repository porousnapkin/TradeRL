using UnityEngine;
using System.Collections;

public abstract class MapAbilityActivatorData : ScriptableObject
{
    public abstract MapAbilityActivator Create();
}
