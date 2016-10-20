public interface ItemEffect
{
    void NumItemsChanged(int numWas, int newNum);
    bool CanActivate();
    void Activate();
}

