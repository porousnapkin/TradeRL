public class GainPoliticalInfluenceWhenRestingTownStatus : EffectAction
{
    public Town affectedTown { private get; set; }
    public int reputationPerDay  { private get; set; }

    public void Apply()
    {
        affectedTown.restModule.PlayerRestedForXDaysEvent += PlayerRestedForXDaysEvent;
    }

    private void PlayerRestedForXDaysEvent(int days)
    {
        affectedTown.politicalReputation.GainXP(days * reputationPerDay);
    }

    public bool CanCombine(EffectAction action)
    {
        return false;
    }

    public void Remove()
    {
        affectedTown.restModule.PlayerRestedForXDaysEvent -= PlayerRestedForXDaysEvent;
    }
}

