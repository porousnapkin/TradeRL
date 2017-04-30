public class ShieldGeneratorEquipment : ItemEffect
{
    public ShieldGeneratorEffect effect { private get; set; }

    public void Equip(Character character)
    {
        effect.Apply(character);
    }

    public void UnEquip(Character character)
    {
        effect.Remove();
    }

    public void NumItemsChanged(int numWas, int newNum) {}
    public bool CanActivate() { return false; }
    public void Activate() {}
    public bool CanEquip() { return true;}
}

