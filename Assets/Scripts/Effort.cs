using UnityEngine;

public class Effort {
    public enum EffortType
    {
        Physical,
        Mental,
        Social
    }

	public event System.Action EffortChangedEvent = delegate{};
	public event System.Action MaxEffortChangedEvent = delegate{};

    [PostConstruct]
    public void PostConstruct()
    {
        var baseStats = BasePlayerCharacterStats.Instance;
        physical = baseStats.basePhysicalPool;
        maxPhysical = baseStats.basePhysicalPool;
        mental = baseStats.baseMentalPool;
        maxMental = baseStats.baseMentalPool;
        social = baseStats.baseSocialPool;
        maxSocial = baseStats.baseSocialPool;
    }

    int physical = 10;
    int mental = 10;
    int social = 10;
    public int Physical
    {
        get
        {
            return physical;
        }
        set
        {
            physical = Mathf.Max(value, 0);
            EffortChangedEvent();
        }
    }

    public int Mental
    {
        get
        {
            return mental;
        }
        set
        {
            mental = Mathf.Max(value, 0);
            EffortChangedEvent();
        }
    }

    public int Social
    {
        get
        {
            return social;
        }
        set
        {
            social = Mathf.Max(value, 0);
            EffortChangedEvent();
        }
    }

    public int GetEffort(EffortType type)
    {
        switch(type)
        {
            case EffortType.Physical:
                return Physical;
            case EffortType.Mental:
                return Mental;
            case EffortType.Social:
                return Social;
        }
        throw new System.Exception("Effort type doesn't exist");
    }

    public void SetEffort(EffortType type, int val)
    {
        switch(type)
        {
            case EffortType.Physical:
                Physical = val;
                break;
            case EffortType.Mental:
                Mental = val;
                break;
            case EffortType.Social:
                Social = val;
                break;
        }
    }

    public void SafeAddEffort(EffortType type, int val)
    {
        var oldVal = GetEffort(type);
        if (oldVal > GetMaxEffort(type))
            return;
        SetEffort(type, Mathf.Min(GetMaxEffort(type), oldVal + val));
    }

    public void SafeSubtractEffort(EffortType type, int val)
    {
        var oldVal = GetEffort(type);
        SetEffort(type, Mathf.Max(0, oldVal - val));
    }

    int maxPhysical = 10;
    int maxMental = 10;
    int maxSocial = 10;
    public int MaxPhysical
    {
        get
        {
            return maxPhysical;
        }
        set
        {
            maxPhysical = value;
            MaxEffortChangedEvent();
        }
    }
    public int MaxMental
    {
        get
        {
            return maxMental;
        }
        set
        {
            maxMental = value;
            MaxEffortChangedEvent();
        }
    }
    public int MaxSocial
    {
        get
        {
            return maxSocial;
        }
        set
        {
            maxSocial = value;
            MaxEffortChangedEvent();
        }
    }

    public int GetMaxEffort(EffortType type)
    {
        switch(type)
        {
            case EffortType.Physical:
                return MaxPhysical;
            case EffortType.Mental:
                return MaxMental;
            case EffortType.Social:
                return MaxSocial;
        }
        throw new System.Exception("Effort type doesn't exist");
    }

    public void SetMaxEffort(EffortType type, int val)
    {
        UnityEngine.Debug.Log("Setting " + type + " to " + val);
        switch(type)
        {
            case EffortType.Physical:
                MaxPhysical = val;
                break;
            case EffortType.Mental:
                MaxMental = val;
                break;
            case EffortType.Social:
                MaxSocial = val;
                break;
        }
    }
}