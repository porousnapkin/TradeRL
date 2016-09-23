using UnityEngine;

public abstract class AmbushActivatorData : ScriptableObject
{
    public abstract AmbushActivator Create();
}