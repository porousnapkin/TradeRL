public class CreateMapAbilityButtonItemEffect : ItemEffect
{
    [Inject] public MapAbilityButtons mapAbilityButtons { private get; set; }
    public MapPlayerAbility mapAbility { private get; set; }

    public void Activate()
    {
    }

    public bool CanActivate()
    {
        return false;
    }

    public void NumItemsChanged(int numWas, int newNum)
    {
        if(numWas <= 0 && newNum > 0)
            mapAbilityButtons.AddButton(mapAbility);
        else if(numWas > 0 && newNum <= 0)
            mapAbilityButtons.RemoveButton(mapAbility);
    }
}