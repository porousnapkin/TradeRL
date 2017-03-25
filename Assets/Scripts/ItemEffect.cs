public interface ItemEffect
{
    void NumItemsChanged(int numWas, int newNum);
    bool CanActivate();
    void Activate();

    bool CanEquip();
    void Equip(Character character);
    void UnEquip(Character character);
}

