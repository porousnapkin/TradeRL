using UnityEngine;

public class EnemyAmbushAbilityData : ScriptableObject
{
    public string abilityName;
    public string description;
    public AmbushActivatorData activator;

    public AmbushActivator Create()
    {
        return activator.Create();
    }
}