public class HasXMoneyRestriction : Restriction
{
    [Inject] public Inventory inventory { private get; set; }
    public int amount;

    public bool CanUse()
    {
        return inventory.Gold >= amount;
    }
}

