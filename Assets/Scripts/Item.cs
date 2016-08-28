public class Item
{
    public ItemEffect effect { private get; set; }
    public string name { private get; set; }
    private int numItems = 0;

    public string GetName()
    {
        return name;
    }

    public int GetNumItems()
    {
        return numItems;
    }

    public void SetNumItems(int newNum)
    {
        var numWas = numItems;
        numItems = newNum;

        effect.NumItemsChanged(numWas, newNum);
    }

    public bool CanActivate()
    {
        return effect.CanActivate();
    }

    public void Activate()
    {
        effect.Activate();
    }
}