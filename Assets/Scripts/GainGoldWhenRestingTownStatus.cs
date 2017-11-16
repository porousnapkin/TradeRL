
public class GainGoldWhenRestingTownStatus : EffectAction
{
    [Inject] public Inventory inventory { private get; set; }
    public Town affectedTown { private get; set; }
    public int goldPerDay  { private get; set; }

    public void Apply()
    {
        affectedTown.restModule.PlayerRestedForXDaysEvent += PlayerRestedForXDaysEvent;
    }

    private void PlayerRestedForXDaysEvent(int days)
    {
        inventory.Gold += goldPerDay;
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

